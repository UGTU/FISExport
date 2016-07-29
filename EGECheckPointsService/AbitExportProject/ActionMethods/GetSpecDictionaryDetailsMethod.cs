using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Controllers;
using AbitExportProject.Data;
using AbitExportProject.ParsersToDB;
using Fdalilib.Actions2016.DictionarySpecDetails;

namespace AbitExportProject.ActionMethods
{
    class GetSpecDictionaryDetailsMethod : BaseProxyMethod<Root, TError, DictionaryData>, IBaseMethod
    {
        protected override string MethodName => "GetSpecDictionaryDetailsMethod";
        public int Year => DateTime.Today.Year;

        public override string ToString()
        {
            return "Импорт данных справочника направлений/специальностей из ФИС в БД УГТУ";
        }

        public bool Run(Func<string, string> askMore)
        {
           Package.GetDictionaryContent.DictionaryCode = MagicNumberController.SpecDictionary;
           var curDict = proxy.ReturnOrNullAndError(Package, "GetDictionaryDetails");

            if (curDict.DictionaryItems == null) return false;
            using (var mainCtx = new UGTUDataDataContext())
            {
                foreach (var dictItem in curDict.DictionaryItems)
                {
                    DictionaryParser.ParseSpecDictionaryItems(mainCtx, dictItem, MagicNumberController.SpecDictionary);
                }
                CommitToDb(mainCtx);
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
