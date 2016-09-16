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
            return new PackageDataCampaignInfo
            {
                Campaigns = mainCtx.Abit_Campaigns.Where(x => x.YearFrom == year).Select(y => new PackageDataCampaignInfoCampaign()
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
                        CampaignInfo = GetCampaignInfo(mainCtx, Year)
                    };

                    var expRes = proxy.ReturnOrNullAndError(Package, "ImportPack");

                    if (expRes == null) return false;

                    SavePackNumber(expRes.PackageID);
                }
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
    }
}
