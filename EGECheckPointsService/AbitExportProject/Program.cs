using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using AbitExportProject.Data;
using Fdalilib;
using Fdalilib.ImportClasses;
using Fdalilib.Service;

namespace AbitExportProject
{
    internal class Program
    {
        /// <summary>
        ///     Производит разбор параметров коммандной строки, упаковывая данные в экземпляр ImportParam
        /// </summary>
        /// <param name="arg">Строка параметров коммандной строки</param>
        /// <returns>Экземпляр ExportParam с данными из командной строки</returns>
        internal static ExportParam ParseArgument(string arg)
        {
            var upperArg = arg.ToUpper().Trim();
            if (String.IsNullOrEmpty(upperArg)) return new ExportParam(ExportType.Undefifined, default(int));
            if (upperArg == "/P") return new ExportParam(ExportType.Batch, default(int));
            if (upperArg.Contains("/ID:"))
            {
                var sParts = upperArg.Split(':');
                Contract.Assert(sParts.Length == 2);
                var id = Convert.ToDecimal(sParts[1].Trim());
                return new ExportParam(ExportType.Single, id);
            }
            throw new InvalidOperationException("Can't parse arg. Check the input string!");
        }     


        internal static void Main(string[] args)
        {
            //CreateEGEPatch();
            //Требуется создавать 2 скрипта: для импорта заявления и для импорта приказов на зачисление

            //GetDictionaries();       //получить справочники из ФИС в логи
            //  GetDictionaryById(35);
            // GetApplicationsStatus();  //получить статусы всех импортированных заявлений

            //DeleteApplication(DateTime.Now.Year, 0);  //удалить заявления
            //DeleteOrders(DateTime.Now.Year);

            // DeleteSPOApplication();
            //DeleteDefiniteApplication(115279);
            /* var mainCtx = CreateDatabaseContext();
             var mainProxy = CreateFisProxy();
             GetImportResultById(mainProxy, 2487080, mainCtx);*/
            //ParseXmlAnswer("mistake.xml");
            /* var mainProxy = CreateFisProxy();
            mainProxy.GetUniversityInfo(6377);*/
                   
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
            }
        }

        private static void GetApplicationsStatus()
        {
            var mainCtx = CreateDatabaseContext();
            var sDao = AbitServiceDao.Instance;
            var impOrders = sDao.OrderOfAdmission();
            int budj = 0;
            foreach (var order in impOrders)
            {
                if (order.FinanceSourceID == 14) budj++;
               /* mainCtx.Export_FB_journals.Single(x => x.nCode == Convert.ToDecimal(order.Application.ApplicationNumber)).StatusID
                    = 1;*/ //поставить признак зачисленных
            }
            Console.Write(budj);
            Console.ReadLine();
            // mainCtx.SubmitChanges();
        }

        private static void CreateEGEPatch()
        {
            var mainCtx = CreateDatabaseContext();
            using (var file = new StreamWriter(@"EGECheck.txt"))
            {
                foreach (var Abit in mainCtx.Export_FB_journals.Where(x => (x.ErrorCode == 1) || (x.ErrorCode == 3015)).OrderBy(y=>y.Person.Clastname))
                {
                    var stud = mainCtx.Persons.FirstOrDefault(y => y.nCode == Abit.nCode);
                    var doc = stud.Doc_studs.FirstOrDefault(y => y.document.IsIdentity);
                    file.WriteLine(stud.Clastname.ToUpper() + "%" + stud.Cfirstname.ToUpper() + "%" + stud.Cotch.ToUpper()
                        + "%" + doc.Cd_seria+"%"+doc.Np_number);
                }
            }
        }

        private static void GetDictionaries()
        {
            var federalDatabase = CreateFisProxy();

            var dicts = federalDatabase.GetDictionaries();
            Console.WriteLine("пошли выкачивать справочники: + {0} штук", dicts.Result.Items.Length);
            foreach (Dictionary item in dicts.Result.Items)
            {
                Console.WriteLine(item.Name);
                var curDict = federalDatabase.GetDictionaryDetails(item.Code);
                Console.WriteLine("Done!");
            }
            Console.ReadLine();
        }

        private static void GetDictionaryById(uint id)
        {
            var federalDatabase = CreateFisProxy();
            federalDatabase.GetDictionaryDetails(id);
            Console.ReadLine();
        }


        private static void DeleteApplication()
        //удалить из их системы все заявления
        {

            var sDao = AbitServiceDao.Instance;
            var impApps = sDao.GetExportedApps().ToArray(); //Where(x=>x.UID==null).
            var delApp = new List<DataForDeleteApplication>();
            var delImd = 0;
            foreach (var application in impApps)
            {
                delApp[delImd] = new DataForDeleteApplication
                    {
                        ApplicationNumber = impApps[delImd].ApplicationNumber,
                        RegistrationDate = impApps[delImd].RegistrationDate
                    };
                delImd++;
            }
            sDao.DeleteApplications(delApp);

        }


        private static void DeleteApplication(int year, int spec_NNRecord)   
        //удалить из их системы все заявления (котоые зафиксированы в нашем Export_FB_journals)
        {
            var ctx = CreateDatabaseContext();
            var sDao = AbitServiceDao.Instance;
            var delApp = new List<DataForDeleteApplication>();

            foreach (var abit in ctx.Export_FB_journals.Where(x=>(x.Registration_Date!=null)&&(x.Registration_Date.Value.Year == year)
                && ((ctx.ABIT_postups.Any(y => (y.ABIT_Diapazon_spec_fac.Main_NNRecord_FB == spec_NNRecord)&&(y.nCode == x.nCode))) || (spec_NNRecord == 0))))
            {
                delApp.Add(new DataForDeleteApplication()
                {
                    ApplicationNumber = Convert.ToString(abit.nCode),
                    RegistrationDate = (DateTime) abit.Registration_Date,
                });
            }

            sDao.DeleteApplications(delApp);
        }

        private static void DeleteOrders(int year)
        //удалить из их системы все приказы
        {
            var ctx = CreateDatabaseContext();
            var sDao = AbitServiceDao.Instance;
            var delOrders = new List<Application>();

            foreach (var abit in ctx.Export_FB_journals.Where(x => (x.Registration_Date != null) && (x.Registration_Date.Value.Year == year)))
            {

                if (ctx.ABIT_postups.Any(y => (y.nCode == abit.nCode)&&(y.ik_prikaz_zach != null)/*&&(y.ABIT_Diapazon_spec_fac.Main_NNRecord_FB == 1777)*/))
                {
                    delOrders.Add(new Application()
                    {
                        ApplicationNumber = abit.nCode.ToString(),
                        RegistrationDate = (DateTime) abit.Registration_Date,
                        NeedHostelSpecified = false,
                        StatusIDSpecified = false
                    });
                }
            }

            sDao.DeleteOrders(delOrders);
        }

        private static void DeleteDefiniteApplication(int nCode)
        //удалить из их системы конкретное заявление
        {
            var ctx = CreateDatabaseContext();
            var sDao = AbitServiceDao.Instance;
            var delApp = new List<DataForDeleteApplication>();


            foreach (var abit in ctx.ABIT_postups.Where(x => (x.nCode == nCode)))
            {
                delApp.Add(new DataForDeleteApplication()
                {
                    ApplicationNumber = Convert.ToString(abit.nCode),
                    RegistrationDate = abit.dd_pod_zayav
                });
            }

            sDao.DeleteApplications(delApp);
        }

        private static void DeleteSPOApplication()
        //удалить из их системы все СПО
        {
          var ctx = CreateDatabaseContext();
          var sDao = AbitServiceDao.Instance;
          var delApp = new List<DataForDeleteApplication>();

          foreach (var abitSPO in ctx.Export_FB_journals.Where(x => ctx.ABIT_postups.Where(y => y.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_direction == 5).Select(z => z.nCode).Contains(x.nCode)))
            {
                delApp.Add(new DataForDeleteApplication()
                    {
                        ApplicationNumber = Convert.ToString(abitSPO.nCode),
                        RegistrationDate = ctx.ABIT_postups.Where(x => x.nCode == abitSPO.nCode).First().dd_pod_zayav
                    });
            }

          sDao.DeleteApplications(delApp);
        }

        /// <summary>
        ///     Производит экспорт в ФИС ЕГЭ данных абитуриентов
        /// </summary>
        private static void ExportBatch(int year)
        {
            var mainCtx = CreateDatabaseContext();
            var mainProxy = CreateFisProxy();

            // GetImportResultByID(mainProxy, 1333273, mainCtx);  //получить результат импорта
             //mainProxy.GetUniversityInfo(6377);
           

            var pack = new PackageData()        //запаковываем все в PackageData
                {
                    //CampaignInfo = Collector.GetCampaignInfo(mainCtx, year, mainProxy.LogWriter),   //экспорт информации о премной кампании (похоже, импортируется только тогда, когда нет активных данных по этапам и датам)
                    //AdmissionInfo = Collector.GetAmissionInfo(mainCtx, year),                       //сведения об объеме и структуре приема   
                    //Applications = Collector.GetCurrentApps(mainCtx, year, 0, 0).ToList(),          //экспорт всех новых абитуриентов: текущего года, с определенным состоянием, которых нет в журнале или потеряли актуальность
                    OrdersOfAdmission = Collector.GetOrdersOfAdmission(mainCtx, year).ToList()      //экспорт приказов на зачисление */
                };

            mainProxy.LogWriter.Write("Всё собрали и пошли отправлять:");
            Console.WriteLine("Всё собрали и пошли отправлять:");
            mainProxy.LogWriter.Flush();
            var expRes = mainProxy.ImportPack(pack).Result; //получить результат экспорта
            if (expRes != null)
            {
                mainProxy.LogWriter.WriteLine("expRes: " + Convert.ToString(expRes.PackageID));
                Console.WriteLine("expRes: " + Convert.ToString(expRes.PackageID));
                mainProxy.LogWriter.WriteLine(expRes.ToString());
            }
            else mainProxy.LogWriter.WriteLine("Похоже, что expRes = null");
            mainProxy.LogWriter.Flush();
            try
            {
                var pakId = expRes.PackageID;               //получить номер экспортированного пакета 
                mainProxy.LogWriter.WriteLine("Номер импортированного пакета: " + Convert.ToString(pakId));
                mainProxy.LogWriter.Flush();
                Console.WriteLine("Номер импортированного пакета: " + Convert.ToString(pakId));
                Console.WriteLine("Теперь жди. Программа завершит работу сама, как только получит результаты импорта");
                GetImportResultById(mainProxy, pakId, mainCtx);

            }
            catch (Exception exception)
            {
                mainProxy.LogWriter.Write(exception.Message);
                mainProxy.LogWriter.Write("MainMetod");
                mainProxy.LogWriter.Flush();
                if (expRes != null)
                {
                    mainProxy.LogWriter.Write(expRes.ToString());
                }
                else Console.ReadLine();

            }
        }

        /// <summary>
        /// После выполненного импорта по оп начинаем запрашивать результаты импорта пакета
        /// </summary>
        /// <param name="mainProxy">Прокси</param>
        /// <param name="pakId">Номер пакета</param>
        /// <param name="mainCtx">контекст</param>
        /// <param name="apps">Заявления, которые отправляли</param>
        private static void GetImportResultById(FisProxy mainProxy, uint pakId, UGTUDataDataContext mainCtx)
        {
            mainProxy.LogWriter.WriteLine("Приступаем к ImportPackResult");
            mainProxy.LogWriter.Flush();
            var tryCount = 0;
            Answer<ImportResultPackage, TError> checkRes;
            try
            {
               checkRes = mainProxy.ImportPackResult(pakId);
               if (checkRes.Result == null)
                {
                    mainProxy.LogWriter.WriteLine("checkRes.Result == null");
                    mainProxy.LogWriter.Flush();  
                }
               mainProxy.LogWriter.WriteLine("ImportPackResult прошел");
               mainProxy.LogWriter.Flush();

               while ((checkRes.Result == null) && (tryCount < 20)) //по номеру получить результат экспорта
                {
                    mainProxy.LogWriter.WriteLine("Пока не импортировано");
                    mainProxy.LogWriter.WriteLine("Приступаем к сну");
                    mainProxy.LogWriter.Flush();
                    System.Threading.Thread.Sleep(100000); //запрашиваем в цикле и ждем, ибо у них на месте пакеты обрабатываются долго
                    checkRes = mainProxy.ImportPackResult(pakId);
                    tryCount++;
                }
            }
            catch (Exception exception)
            {
                mainProxy.LogWriter.WriteLine(exception.Message);
                mainProxy.LogWriter.Write("SubMetod");
                mainProxy.LogWriter.Flush();
                return;
            }

            if (checkRes.Result == null) return;

            ParseImportResult(mainProxy, mainCtx, DateTime.Now.Year, checkRes);
        }

        private static void ParseImportResult(FisProxy mainProxy, UGTUDataDataContext mainCtx, int year, Answer<ImportResultPackage, TError> checkRes)
        {
            IEnumerable<decimal> nonExpNCode;

            //ошибки по приказам
            if (checkRes.Result.Log.Failed.OrdersOfAdmissions != null)
            {
                foreach (var badApp in checkRes.Result.Log.Failed.OrdersOfAdmissions)
                {
                    var jourRecord =
                        mainCtx.Export_FB_journals.FirstOrDefault(x => x.nCode.ToString() == badApp.ApplicationNumber);
                    jourRecord.Prikaz_result = badApp.ErrorInfo.Message;

                    jourRecord.ErrorCode = (int?) badApp.ErrorInfo.ErrorCode;
                    jourRecord.Is_actual = false;
                }
            }

            //ошибки по импорту заявлений
            if (checkRes.Result.Log.Failed.Applications != null)
            {
                foreach (var badApp in checkRes.Result.Log.Failed.Applications)
                {
                    var jourRecord =
                        mainCtx.Export_FB_journals.FirstOrDefault(x => x.nCode.ToString() == badApp.ApplicationNumber);
                    jourRecord.Import_result = badApp.ErrorInfo.Message;

                    jourRecord.ErrorCode = (int?)badApp.ErrorInfo.ErrorCode;
                    jourRecord.Is_actual = false;
                }
            }
            mainCtx.SubmitChanges();
        }

        /// <summary>
        ///     Производит экспорт в ФИС ЕГЭ данных заданного абитуриента
        /// </summary>
        /// <param name="exportParam">Параметры абитуриента для экспорта</param>
        internal static void ExportSingle(ExportParam exportParam, int year)
        {
            Contract.Requires(exportParam.ExportType == ExportType.Single);
            using (var ctx = CreateDatabaseContext())
            {
                var application = Collector.BuildApplicationPackage(exportParam.AbitId, ctx, year);
                Contract.Assert(application != null);
                var fisProxy = CreateFisProxy();
                fisProxy.ImportSingle(application);
            }
        }

        internal static void ExportUnexpoted()
        {
            var ctx = CreateDatabaseContext();

            var allApps = Collector.GetCurrentApps(ctx, 2014, 0, 0);
            var unExportedAppsUid = allApps.Select(x => x.UID).Except(AbitServiceDao.Instance.GetExportedApps().Select(x => x.UID));
            var unexportedApps = allApps.Where(x => unExportedAppsUid.Contains(x.UID));
            var pack = new PackageData() { Applications = unexportedApps.ToList() };

            CreateFisProxy().ImportPack(pack);
        }

        /// <summary>
        ///     Создаёт экземпляр контекста для доступа к данным ИС УГТУ
        /// </summary>
        /// <returns>Экземпляр UGTUDataDataContext</returns>
        private static UGTUDataDataContext CreateDatabaseContext()
        {
            return new UGTUDataDataContext();
        }

        /// <summary>
        ///     Создаёт экземпляр прокси объекта для доступа к сервисам ФИС ЕГЭ с прараметрами по умолчанию
        /// </summary>
        /// <returns>Экземпляр FisProxy</returns>
        private static FisProxy CreateFisProxy()
        {
           // WebRequest.DefaultWebProxy = new WebProxy("http://195.22.104.27:3128/", true);
            //WebRequest.DefaultWebProxy = new WebProxy("http://195.133.189.6:8080/", true);
            IFisProxyService service = new WebClientFisProxyService(new WebClient(), new Uri("http://10.0.3.1:8080/import/"));

            var federalDatabase = new FisProxy("pk@ugtu.net", "2lvFeaJ", service);
            federalDatabase.LogWriter =
                File.CreateText("OutLogs/" + ((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString() + "log.txt");
            return federalDatabase;
        }

        public static void ParseXmlAnswer(string fName)
        {
            var mainCtx = CreateDatabaseContext();
            var mainProxy = CreateFisProxy();

            var doc = XDocument.Load(fName);

            var res = XElement.Parse(doc.Element("ImportResultPackage").ToString());


            var checkRes = FisProxy.Deserialize<ImportResultPackage, TError>(res);
            ParseImportResult(mainProxy, mainCtx, DateTime.Now.Year, checkRes);
        }

        /// <summary>
        /// </summary>
        internal struct ExportParam
        {
            private readonly decimal _abitId;
            private readonly ExportType _exportType;

            public ExportParam(ExportType exporttParamType, decimal abitId)
            {
                _exportType = exporttParamType;
                _abitId = abitId;
            }

            public ExportType ExportType
                {
                get { return _exportType; }
                }

            public decimal AbitId
                {
                get { return _abitId; }
                }
            }

        /// <summary>
        ///     Определяет тип экспорта
        /// </summary>
        internal enum ExportType
        {
            /// <summary>
            ///     Экспортируется заданный абитуриент
            /// </summary>
            Single,

            /// <summary>
            ///     Экспортируется пакет заявлений на основе системного журнала
            /// </summary>
            Batch,
            /// <summary>
            /// Необходимые параметры не переданы
            /// </summary>
            Undefifined
        }
        }

      
}
