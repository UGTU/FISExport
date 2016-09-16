using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using AbitExportProject.ParsersToDB;
using Fdalilib.Actions2016.DictionaryDetails;


namespace AbitExportProject.ActionMethods
{
    class GetDictionaryDetailsMethod : BaseProxyMethod<Root, TError, DictionaryData>, IBaseMethod
    {
        private string _question = "Введите Код справочника:";
        public uint DictId {
            get { return Package.GetDictionaryContent.DictionaryCode; } 
            set { Package.GetDictionaryContent.DictionaryCode = value; } } 

        public override string ToString()
        {
            return "Импорт данных одного справочника из ФИС в БД УГТУ";
        }

        public int Year => DateTime.Today.Year;

        protected override string MethodName => "GetDictionaryDetailsMethod";


        public bool Run(Func<string, string> askMore)
        {
            try
            {
                if (askMore != null)
                {
                    int dictId;
                    if (int.TryParse(askMore(_question), out dictId)) DictId = (uint)dictId;
                }

                var curDict = proxy.ReturnOrNullAndError(Package, "GetDictionaryDetails");

                if (curDict.DictionaryItems == null) return false;

                using (var mainCtx = new UGTUDataDataContext())
                {
                    foreach (var dictItem in curDict.DictionaryItems)
                    {
                        DictionaryParser.ParseDictionaryItems(mainCtx, dictItem, (int)DictId);
                    }
                    CommitToDb(mainCtx);
                }
            }
            catch(Exception)
            {
                return false;
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
