using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib.Actions2016.BatchApplicationImport;

namespace AbitExportProject.ActionMethods
{
    class ExportCampaignInfoMethod: BaseProxyMethod<Root, TError, ImportPackageInfo>, IBaseMethod
    {
        protected override string MethodName => "ExportCampaignInfoMethod";
        public override string ToString()
        {
            return "Экспортировать информацию о премной кампании";
        }

        public int Year => DateTime.Today.Year;

        public static PackageDataCampaignInfo GetCampaignInfo(UGTUDataDataContext mainCtx, int year)
        {
            //var camp = mainCtx.Abit_Campaigns.Where(x => x.YearFrom == year).ToList();

            return new PackageDataCampaignInfo
            {
                Campaigns = mainCtx.Abit_Campaigns.Where(x => x.YearFrom == year).ToList().Select(y => new PackageDataCampaignInfoCampaign()
                        {
                            CampaignTypeID = (uint) y.TypePK.IDItem,
                            UID = y.UID.ToString(),
                            Name = y.Name,
                            EducationForms = y.Abit_Campaign_FormEds.Select(form => (uint)form.DictionaryContent.IDItem).ToList(),
                            EducationLevels = y.Abit_Campaign_Directions.Select(level => (uint)level.DictionaryContent.IDItem).ToList(),
                            StatusID = (uint)y.StatusPK.IDItem,
                            YearEnd = (uint)y.YearFrom,
                            YearStart = (uint)y.YearTo
                        }).ToList()
            };


            /*
        try
        {
           var forms = new List<uint>();
           var levels = new List<uint>();
           var currCampain = mainCtx.Abit_Campaigns.Single(x => x.YearFrom == year);
           logger.WriteLine("Импорт по кампании: {0}", currCampain.Name);
           logger.Flush();

           //собираем даты приемной кампании
           var campContent = mainCtx.Abit_Campaign_Contents.Where(x => x.NNyear == year);
           var campdDates = campContent.Select(camp => new PackageDataCampaignInfoCampaignCampaignDate()
           {
               UID = camp.ID_Content.ToString(),
               Course = 1,
               DateStart = camp.Date_start.Value.Date.ToString(),
               DateEnd = camp.Date_end.Value.Date.ToString(),
               DateOrder = camp.Date_order.Value.Date.ToString(),
               EducationFormID = (uint)camp.Form_ed.ik_FB,
               EducationLevelID = (uint)camp.Direction.ik_FB,
               EducationSourceID = (uint)camp.TypeKatZach.ik_FB,
           }).ToList();
           logger.WriteLine("Собрали даты в campdDates");
           logger.Flush();

           foreach (var abContent in campContent.Where(x => x.stage.HasValue))
           {
               if (abContent.stage.HasValue)
                   campdDates.Single(y => y.UID == abContent.ID_Content.ToString()).Stage_zach = (uint)abContent.stage.Value;
           }
           logger.WriteLine("Поколдовали с этапами зачисления");
           logger.Flush();

           foreach (var camp in campContent)
           {
               //собираем формы обучения
               if ((!forms.Exists(x => x == camp.Form_ed.ik_FB)) && (camp.Form_ed.ik_FB != null))
                   forms.Add((uint)camp.Form_ed.ik_FB);
               //собираем уровни подготовки
               if ((!levels.Exists(y => y.EducationLevelID == camp.Direction.ik_FB)) && (camp.Direction.ik_FB != null))
                   levels.Add(new PackageDataCampaignInfoCampaignEducationLevel()
                   {
                       Course = 1,
                       EducationLevelID = (uint)camp.Direction.ik_FB
                   });
           }
           logger.WriteLine("Собрали формы обучения и уровни подготовки");
           logger.Flush();

           var campInfo = new PackageDataCampaignInfo();
           var campItems = new List<PackageDataCampaignInfoCampaign>
        {
           new PackageDataCampaignInfoCampaign()
           {
               UID = currCampain.UID.ToString(),
               Name = currCampain.Name,
               StatusID = 1, //1 - Идет набор, 2 - Завершена
               YearStart = (uint) year,
               YearEnd = (uint) year,
               CampaignDates = campdDates,
               EducationForms = forms,
               EducationLevels = levels
           }
        };
           logger.WriteLine("Собрали даты в campInfo");
           logger.Flush();

           campInfo.Campaigns = campItems;
           return campInfo;
        }
        catch (Exception exception)
        {
           logger.Write(exception.Message);
           throw;
        }*/

        }

        protected override void SetAuth()
        {
            Package.AuthData.Login = Login;
            Package.AuthData.Pass = Password;
        }

        public void Run(Func<string, string> askMore)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {

                //GetImportResultByID(mainProxy, 1333273, mainCtx);  //получить результат импорта
                //mainProxy.GetUniversityInfo(6377);

                Package.PackageData = new PackageData() { 
                    CampaignInfo = GetCampaignInfo(mainCtx, Year)
                };

                var expRes = proxy.ReturnOrNullAndError(Package, "ImportPack");

                SavePackNumber(expRes.PackageID);

                // var expRes = proxy.ExportBatch(pack)
                //RepeatExportTillResult(pack);
            }
        }
    }
}
