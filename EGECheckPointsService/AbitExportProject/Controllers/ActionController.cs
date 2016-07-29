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
        private readonly List<IBaseMethod> _actionList;

        /// <summary>
        /// В конструкторе должны быть зарегистрированы по имени типа те методы, которые доступны для вызова пользователем
        /// </summary>
        public ActionController()           
        {
            _actionList = new List<IBaseMethod>();
            RegisterMethod(typeof (GetDictionaryMethod));
            RegisterMethod(typeof(GetSpecDictionaryDetailsMethod));
            RegisterMethod(typeof (GetDictionaryDetailsMethod));
            RegisterMethod(typeof(CreateEGEPackMethod));
            RegisterMethod(typeof(ExportCampaignInfoMethod));
            RegisterMethod(typeof(AdmissionVolumeImportMethod));
            RegisterMethod(typeof(TargetOrganizationImportMethod));
            RegisterMethod(typeof(CompetitiveGroupsImportMethod));
            RegisterMethod(typeof(ApplicationsImportMethod));
            RegisterMethod(typeof(OrdersImportMethod));
            RegisterMethod(typeof(GetUnexportedAbitsMethod));
            RegisterMethod(typeof(GetImportResultMethod));
        }

        public void RegisterMethod(Type objType)
        {
            _actionList.Add((IBaseMethod)Activator.CreateInstance(objType));
        }

        /// <summary>
        /// Операция, которая передается в метод, если ему необходимо задать пользователю дополнительные уточняющие вопросы. Возвращает ответ в ввиде строки.
        /// </summary>
        /// <param name="question">Дополнительный вопрос, который задается методом</param>
        /// <returns></returns>
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

        public void MakeAction(string action)
        {
            int idAction;
            if (!int.TryParse(action, out idAction))
            {
                Console.WriteLine("Вы выбрали недопустимую операцию. Работа программы будет завершена");
                return;
            }
            if (!_actionList.ToArray()[idAction].Run(AskByConsole))
            {
                Console.WriteLine("Операция прошла с ошибками. Смотрите результаты в папке OutLogs");
            };

        }
    }
}
