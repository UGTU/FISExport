using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.ActionMethods;
using AbitExportProject.Data;
using AbitExportProject.ParsersToDB;
using Fdalilib.Actions2016.Dictionary;


namespace AbitExportProject.ActionMethods
{
    class GetDictionaryMethod: BaseProxyMethod<Root, TError, Fdalilib.Actions2016.Dictionary.Dictionaries>, IBaseMethod
    {
        const int OlympicDictionary = 19;  //справочник олимпиад

        public override string ToString()
        {
            return "Импорт всех справочников из ФИС в БД УГТУ";
        }

        public int Year => DateTime.Today.Year;

        protected override string MethodName => "GetDictionaryMethod";

        public void Run(Func<string, string> askMore)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                var dicts = proxy.ReturnOrNullAndError(Package, "GetDictionaries");

                Console.WriteLine("пошли выкачивать справочники: + {0} штук", dicts.Items.Count);
                foreach (DictionariesDictionary dictionary in dicts.Items)
                {
                    Console.WriteLine(dictionary.Name);  
                    DictionaryParser.ParseDictionary(mainCtx, dictionary);

                    CommitToDb(mainCtx);

                    if (dictionary.Code == OlympicDictionary)
                    {
                        var dictDetailmethod = new GetOlympicDictionaryDetailsMethod() {DictId = dictionary.Code};
                        dictDetailmethod.Run(null);
                    }
                    else
                    {
                        var dictDetailmethod = new GetDictionaryDetailsMethod {DictId = dictionary.Code};
                        dictDetailmethod.Run(null);
                    }                  
                }         
            }
        }

        public void GetDictionaryById(uint id)
        {
            //sDao.DictionaryDetails(id);
            //Console.ReadLine();
        }
    }
}
