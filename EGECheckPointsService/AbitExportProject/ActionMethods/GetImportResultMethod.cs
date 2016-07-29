using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib.Actions2016.GetImportResult;


namespace AbitExportProject.ActionMethods
{
    class GetImportResultMethod : BaseProxyMethod<Root, TError, ImportResultPackage>, IBaseMethod
    {
        private string _question = "Введите номер пакета:";
        protected override string MethodName => "GetImportResultMethod";

        private uint PackID
        {
            get { return Package.GetResultImportApplication.PackageID; }
            set { Package.GetResultImportApplication.PackageID = value; }
        }

        public override string ToString()
        {
            return "Получить результаты импорта...";
        }
        public int Year => DateTime.Today.Year;

        public bool Run(Func<string, string> askMore)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                int packId;
                if ((askMore == null)|| !int.TryParse(askMore(_question), out packId))
                    packId = Convert.ToInt32(mainCtx.PackageImports.OrderByDescending(x => x.ImportTime).First().id);

                PackID = (uint) packId;

                var Result = proxy.ReturnOrNullAndError(Package, "ImportPackResult");
            }
            return true;
        }
        protected override void SetAuth()
        {
            Package.AuthData.Login = Login;
            Package.AuthData.Pass = Password;
        }
    }
}
