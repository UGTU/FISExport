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
            Console.WriteLine("0 - Импорт всех справочников из ФИС в БД УГТУ");
            Console.WriteLine("1 - Импорт данных справочника направлений/специальностей из ФИС в БД УГТУ");
            Console.WriteLine("2 - Импорт данных одного справочника из ФИС в БД УГТУ");
            Console.WriteLine("3 - Сформировать XML-пакет для проверки ЕГЭ абитуриентов...");
            Console.WriteLine("4 - Экспортировать информацию о премной кампании");
            Console.WriteLine("5 - Экспортировать информацию об объеме и структуре приема");
            Console.WriteLine("6 - Экспортировать информацию об организациях целевого приема");
            Console.WriteLine("7 - Экспортировать информацию о конкурсных группах");
            Console.WriteLine("8 - Экспортировать новые и изменившиеся заявления");
            Console.WriteLine("9 - Экспортировать приказы о зачислении");
            Console.WriteLine("10 - Получить список nCode абитуриентов, которые по каким-то причинам не были импортированы");
            Console.WriteLine("11 - Получить результаты импорта");
            */
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
