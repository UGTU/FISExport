using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.ActionMethods;
using AbitExportProject.Controllers;
using AbitExportProject.Data;
using AbitExportProject.ParsersToDB;
using Fdalilib.Actions2016.Dictionary;


namespace AbitExportProject.ActionMethods
{
    class GetDictionaryMethod: BaseProxyMethod<Root, TError, Dictionaries>, IBaseMethod
    {

        public override string ToString()
        {
            return "Импорт всех справочников из ФИС в БД УГТУ";
        }

        public int Year => DateTime.Today.Year;

        protected override string MethodName => "GetDictionaryMethod";

        public bool Run(Func<string, string> askMore)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                try
                {
                    var dicts = proxy.ReturnOrNullAndError(Package, "GetDictionaries");

                    if (dicts?.Items == null) return false;

                    Console.WriteLine("пошли выкачивать справочники: + {0} штук", dicts.Items.Count);
                    foreach (DictionariesDictionary dictionary in dicts.Items)
                    {
                        Console.WriteLine(dictionary.Name);
                        DictionaryParser.ParseDictionary(mainCtx, dictionary);

                        CommitToDb(mainCtx);
                        IBaseMethod dictDetailmethod;
                        switch (dictionary.Code)
                        {
                            case MagicNumberController.OlympicDictionary:
                                dictDetailmethod = new GetOlympicDictionaryDetailsMethod();
                                break;
                            case MagicNumberController.SpecDictionary:
                                dictDetailmethod = new GetSpecDictionaryDetailsMethod();
                                break;
                            default:
                                dictDetailmethod = new GetDictionaryDetailsMethod { DictId = dictionary.Code };
                                break;
                        }
                        dictDetailmethod.Run(null);
                    }
                }
                catch(Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public void GetDictionaryById(uint id)
        {
            //sDao.DictionaryDetails(id);
            //Console.ReadLine();
        }
    }
}
