using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib.Actions2016.BatchApplicationImport;

namespace AbitExportProject.ActionMethods
{
    class TargetOrganizationImportMethod : BaseProxyMethod<Root, TError, ImportPackageInfo>, IBaseMethod
    {
        protected override string MethodName => "TargetOrganizationImportMethod";
        public int Year => DateTime.Today.Year;
        public override
        string ToString()
        {
            return "Экспортировать информацию об организациях целевого приема";
        }
        protected override void SetAuth()
        {
            Package.AuthData.Login = Login;
            Package.AuthData.Pass = Password;
        }

        public bool Run(Func<string, string> askMore)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                Package.PackageData = new PackageData()
                {
                    TargetOrganizations = GetTargetOrganization(mainCtx, Year)
                };

                var expRes = proxy.ReturnOrNullAndError(Package, "ImportPack");

                if (expRes == null) return false;
                SavePackNumber(expRes.PackageID);
                return true;
            }
        }

        private List<PackageDataTargetOrganization> GetTargetOrganization(UGTUDataDataContext mainCtx, int year)
        {
            return mainCtx.ABIT_postups.Where(x => (x.idTarget != null) && (x.ABIT_Diapazon_spec_fac.NNyear == year))
                .Select(y => y.Abit_Target.TargetOrganization).ToList().Distinct()
                .Select(org => new PackageDataTargetOrganization()
                {
                    UID = org.id.ToString(), Name = org.OrganizationName
                }).ToList();
        }
    }
}
