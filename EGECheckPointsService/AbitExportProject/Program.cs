using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using AbitExportProject.ActionMethods;
using AbitExportProject.Controllers;
using AbitExportProject.Data;
using AbitExportProject.ParsersToDB;
using Fdalilib;
using Fdalilib.Service;

namespace AbitExportProject
{
    internal class Program
    {   
        internal static void Main(string[] args)
        {
            var actController = new ActionController();
            actController.PrintPossibleActions(); //вывести все возможные действия
            var action = Console.ReadLine();
            actController.MakeAction(action.Trim());
            Console.WriteLine("Операция выполнена. Нажмите [ENTER]...");
            Console.ReadLine();

            // CreateEGEPatch();
            // Требуется создавать 2 скрипта: для импорта заявления и для импорта приказов на зачисление

            // GetDictionaryById(35);
            // GetApplicationsStatus();  //получить статусы всех импортированных заявлений

            //DeleteApplication(DateTime.Now.Year, 0);  //удалить заявления
            //DeleteOrders(DateTime.Now.Year);

            // DeleteSPOApplication();
            //DeleteDefiniteApplication(115279);

            //  GetImportResultById(mainProxy, 2487080, mainCtx);
            //ParseXmlAnswer("mistake.xml");
            //mainProxy.GetUniversityInfo(6377);
            /*     
#if DEBUG
          // Для тестирования можно использовать явную задачу параметров 
          //var param = new ExportParam(ExportType.Single, 67627);
          var param = new ExportParam(ExportType.Batch, 0);
#else
          // Получение параметров из коммандной строки
          var param = ParseArgument(args.Length == 1 ? args[0] : string.Empty);
#endif

          switch (param.ExportType)
          {
              // Экспорт данных по одному абитуриенту
              case ExportType.Single:
                  ExportSingle(param, DateTime.Now.Year);
                  break;
              // Экспорт данных по всем абитуриентам, данные по которым не эспортировались или были изменены с момента предыдущего экспорта
              case ExportType.Batch:
                  ExportBatch(DateTime.Now.Year);
                  break;
              default:
                  // Утилита позволяет производить экспорт заявлений указанного студента, либо всех студентов, данные по которым не экспортировались в ФИС ЕГЭ
                  // либо были изменены с момена прошедшего импорта
                  Console.WriteLine("Usage: {0} [/Id:<abitId> | /p ]",
                                    AppDomain.CurrentDomain.SetupInformation.ApplicationName);
                 return;
          }*/

            //GetImportResultByID(mainProxy, 1333273, mainCtx);  //получить результат импорта
            //mainProxy.GetUniversityInfo(6377);

            // var expRes = proxy.ExportBatch(pack)
            //RepeatExportTillResult(pack);
        }
    }     
}
