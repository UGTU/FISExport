using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.ActionMethods;

namespace AbitExportProject.Controllers
{
    public class ActionController
    {
        private List<IBaseMethod> _actionList;

        public ActionController()
        {
            _actionList = new List<IBaseMethod>();
            RegisterMethod(typeof (GetDictionaryMethod));
            RegisterMethod(typeof (GetDictionaryDetailsMethod));
            RegisterMethod(typeof(CreateEGEPackMethod));
            RegisterMethod(typeof(ExportCampaignInfoMethod));
            RegisterMethod(typeof(GetImportResultMethod));
        }

        public void RegisterMethod(Type objType)
        {
            _actionList.Add((IBaseMethod)Activator.CreateInstance(objType));
        }

        public string AskByConsole(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        public void PrintPossibleActions()
        {
            Console.WriteLine("Выберите нужную операцию:");
            var arr = _actionList.ToArray();
            for (var i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(@"{0} - {1}", i, arr[i].ToString());
            }

           
            /*
            Console.WriteLine("0 - Импорт справочников из ФИС в БД УГТУ");
            Console.WriteLine("1 - Сформировать XML-пакет для проверки ЕГЭ абитуриентов...");
            Console.WriteLine("2 - Экспортировать информацию о премной кампании");
            Console.WriteLine("3 - Экспортировать сведения об объеме и структуре приема");
            Console.WriteLine("4 - Экспортировать заявление конкретного абитуриента...");
            Console.WriteLine("5 - Экспортировать новые и изменившиеся заявления");
            Console.WriteLine("6 - Экспортировать приказы на зачисления");
            Console.WriteLine("7 - Удалить конкретное заявление из ФИС...");
            Console.WriteLine("8 - Удалить все заявления из ФИС");
            Console.WriteLine("9 - Удалить заявления СПО из ФИС");
            Console.WriteLine("10 - Удалить из ФИС все заявления по конкретному набору...");
            Console.WriteLine("11 - Удалить из ФИС все приказы на зачисление");*/
        }

        private static bool IsCorrectIndex(string code)
        {
            try
            {
                Convert.ToInt32(code);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Некорректный код");
                return false;
            }
            
        }

        public void MakeAction(string action)
        {
            int idAction;
            if (int.TryParse(action, out idAction))
            {
                _actionList.ToArray()[idAction].Run(AskByConsole);
            }
            /*var bc = new BaseController();

            
            switch (action)
            {
                case "0":
                {
                    bc.GetDictionaries(); //получить справочники из ФИС в БД
                    break;
                }
                case "1":
                {
                    Console.WriteLine("Введите имя файла:");
                    bc.CreateEgeXMLPatch(Console.ReadLine()); //cформировать XML-пакет абитуриентов для проверки ЕГЭ
                    break;
                }
                case "2":
                {
                    bc.ExportCampaignInfo(DateTime.Today.Year);
                        break;
                }
                case "3":
                    {
                        bc.ExportAdmissionInfo(DateTime.Today.Year);
                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("Введите код абитуриента:");
                        var nCode = Console.ReadLine();
                        if (IsCorrectIndex(nCode))
                            bc.ExportSingle(Convert.ToInt32(nCode), DateTime.Today.Year); //удалить абитуриента из ФИС по ID
                        break;
                    }
                case "5":
                    {
                        bc.ExportApplications(DateTime.Today.Year);
                        break;
                    }
                case "6":
                    {
                        bc.ExportOrdersBatch(DateTime.Today.Year);
                        break;
                    }
                case "7":
                    {
                        Console.WriteLine("Введите код абитуриента:");
                        var nCode = Console.ReadLine();
                        if (IsCorrectIndex(nCode))
                            bc.DeleteDefiniteApplication(Convert.ToInt32(nCode)); //удалить абитуриента из ФИС по ID
                        break;
                    }
                case "8":
                    {
                        bc.DeleteExportedApplications();
                        break;
                    }
                case "9":
                    {
                        bc.DeleteSPOApplications();
                        break;
                    }
                case "10":
                    {
                        Console.WriteLine("Введите NNRecord набора:");
                        var NNRecord = Console.ReadLine();
                        if (IsCorrectIndex(NNRecord))
                            bc.DeleteApplicationByNNRecord(Convert.ToInt32(NNRecord));
                        break;
                    }
                case "11":
                    {
                        bc.DeleteOrders(DateTime.Today.Year);
                        break;
                    }
                default: Console.WriteLine("Вы выбрали недопустимую операцию");
                    break;
            } */

        }
    }
}
