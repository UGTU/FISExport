using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib.Actions2016.GetUniversityProperty;


namespace AbitExportProject.ActionMethods
{
    class GetUnexportedAbitsMethod : BaseProxyMethod<Root, TError, InstitutionExports>, IBaseMethod
    {
        public int Year => DateTime.Today.Year;

        protected override string MethodName => "GetUnexportedAbitsMethod";

        public override string ToString()
        {
            return "Получить список nCode абитуриентов, которые по каким-то причинам не были импортированы";
        }
        public bool Run(Func<string, string> askMore)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                var impApps = proxy.ReturnOrNullAndError(Package, "GetUniversityInfo").InstitutionExport.Applications;  //все экспортированные заявления
                var allApps = mainCtx.Export_FB_journals.Where(x => x.NNYear == Year);                                  //все наши заявления
                foreach (var abit in allApps.Select(x => x.nCode.ToString()).Except(impApps.Select(y => y.UID)))
                {
                  MakeLog(abit);   
                }
                return true;
            }
        }
    }
}
