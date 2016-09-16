using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Controllers;
using AbitExportProject.Data;
using Fdalilib.Actions2016.BatchApplicationImport;

namespace AbitExportProject.ActionMethods
{
    class AdmissionVolumeImportMethod : BaseProxyMethod<Root, TError, ImportPackageInfo>, IBaseMethod
    {
        protected override string MethodName => "AdmissionVolumeImportMethod";
        public int Year => DateTime.Today.Year;
        public override string ToString()
        {
            return "Экспортировать информацию об объеме и структуре приема";
        }
        protected override void SetAuth()
        {
            Package.AuthData.Login = Login;
            Package.AuthData.Pass = Password;
        }

        public bool Run(Func<string, string> askMore)
        {
            try
            {
                using (var mainCtx = new UGTUDataDataContext())
                {
                    Package.PackageData = new PackageData()
                    {
                        AdmissionInfo = GetAdmissionVolumeInfo(mainCtx, Year)
                    };

                    var expRes = proxy.ReturnOrNullAndError(Package, "ImportPack");

                    if (expRes == null) return false;

                    SavePackNumber(expRes.PackageID);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private PackageDataAdmissionInfo GetAdmissionVolumeInfo(UGTUDataDataContext mainCtx, int year)
        {
            //выбираем все кампании года
            var campaigns = mainCtx.Abit_Campaigns.Where(x => x.YearFrom == year).ToList();

            var admVolume = new List<PackageDataAdmissionInfoItem>();
            //заполняем данные по каждой кампании
            foreach (var campain in campaigns)
            {
                //все направления по поступлению, соответствующие типу кампании
                var nabors = mainCtx.ABIT_Diapazon_spec_facs.Where(x => x.NNyear == year)
                    .Where(x => x.Relation_spec_fac.EducationBranch.Direction.TypeDirection.ik_FIS_TypePK == campain.ik_FIS_TypePK);

                //IDs из ФИС для указанной кампании
                var abitSpecs = nabors.Select(x => x.Relation_spec_fac.EducationBranch).Select(x=>x.ik_FB).ToList().Distinct();

                //выгружаем все контр цифры приема по специальностям для кампании
                foreach (var educBranch in abitSpecs)
                {
                    var allNaborsBySpec = nabors.Where(x => x.Relation_spec_fac.EducationBranch.ik_FB == educBranch);
                    //за образец берется специальность с наименьшим именем
                    var ourSpec = mainCtx.EducationBranches.Where(x => x.ik_FB == educBranch).OrderBy(x=>x.Cname_spec.Length).FirstOrDefault();        
                    
                    //Debug.Assert(ourSpec != null, "OurSpec != null");
                    if (ourSpec == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не найдена специальность для {0}", nabors.Select(x => x.Relation_spec_fac.EducationBranch).First(x => x.ik_FB == educBranch).Cname_qualif));

                    var levelIk = ourSpec.Direction.ik_FB;
                    
                    //Debug.Assert(levelIk != null, "levelIk != null");
                    if (levelIk == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указан код для {0} по ФИС", ourSpec.Direction.cName_direction));

                    var AdmissionInfo = new PackageDataAdmissionInfoItem
                    {
                        UID = ourSpec.ik_spec.ToString(), //abitSpec.NNrecord.ToString(),
                        CampaignUID = campain.UID.ToString(),
                        EducationLevelID = (uint)ourSpec.Direction.ik_FB,
                        DirectionID = (uint)educBranch,
                        //бюджет
                        NumberBudgetO = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestBudjet) ?? 0),
                        NumberBudgetZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestBudjet) ?? 0),
                        NumberBudgetOZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestBudjet) ?? 0),
                        //договор (устанавливаем плановые цифры только в том случае, если набор по данной форме обучения есть)
                        NumberPaidO = (uint)(allNaborsBySpec.Count(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)) > 0 ? 100 : 0),
                        NumberPaidZ = (uint)(allNaborsBySpec.Count(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)) > 0 ? 100 : 0),
                        NumberPaidOZ = (uint)(allNaborsBySpec.Count(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)) > 0 ? 100 : 0),
                        //ЦКП
                        NumberTargetO = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        NumberTargetZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        NumberTargetOZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        //особое право
                        NumberQuotaO = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        NumberQuotaZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        NumberQuotaOZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0)
                    };
                    //отправляем только там, где есть цифры приема
                    AdmissionInfo.NumberBudgetOSpecified = AdmissionInfo.NumberBudgetO > 0;
                    AdmissionInfo.NumberBudgetOZSpecified = AdmissionInfo.NumberBudgetOZ > 0;
                    AdmissionInfo.NumberBudgetZSpecified = AdmissionInfo.NumberBudgetZ > 0;
                    AdmissionInfo.NumberPaidOSpecified = AdmissionInfo.NumberPaidO > 0;
                    AdmissionInfo.NumberPaidOZSpecified = AdmissionInfo.NumberPaidOZ > 0;
                    AdmissionInfo.NumberPaidZSpecified = AdmissionInfo.NumberPaidZ > 0;
                    AdmissionInfo.NumberQuotaOSpecified = AdmissionInfo.NumberQuotaO > 0;
                    AdmissionInfo.NumberQuotaOZSpecified = AdmissionInfo.NumberQuotaOZ > 0;
                    AdmissionInfo.NumberQuotaZSpecified = AdmissionInfo.NumberQuotaZ > 0;

                    admVolume.Add(AdmissionInfo);
                }
            }
            var admissionInfo = new PackageDataAdmissionInfo()
            {
                AdmissionVolume = admVolume
            };

            return admissionInfo;
        }
    }
}
