using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AbitExportProject.Data;
using AbitExportProject.ParsersToDB;
using Fdalilib;
using Fdalilib.Actions2016.Authorization;
using Fdalilib.Service;
using static System.String;


namespace AbitExportProject.ActionMethods
{

    public class BaseProxyMethod<TInput, TError, TReturn> where TInput : class, new() where TError : class where TReturn : class
    {
        private IFisProxyService _service;
        protected TInput Package;
        protected AuthData AuthData;

        protected string Login => "pk@ugtu.net";
        protected string Password => "2lvFeaJ";

        /// <summary>
        ///     Производит разбор параметров коммандной строки, упаковывая данные в экземпляр ImportParam
        /// </summary>
        /// <param name="arg">Строка параметров коммандной строки</param>
        /// <returns>Экземпляр ExportParam с данными из командной строки</returns>
        internal static ExportParam ParseArgument(string arg)
        {
            var upperArg = arg.ToUpper().Trim();
            if (IsNullOrEmpty(upperArg)) return new ExportParam(ExportType.Undefifined, default(int));
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

        private FisProxy<TInput, TError, TReturn> _proxy;

        /// <summary>
        /// Создаёт экземпляр корнегого элемента с данными аутентификации, переданными в параметрах конструктора, для использования в качестве параметра к сервису ФИС ЕГЭ
        /// </summary>
        /// <returns>Экземпляр Root </returns>
        private void CreateRootAuth()
        {
            Package = new TInput();
            SetAuth();
        }

        protected virtual string MethodName => "BaseProxyMethod";

        private void CreateFisProxy()
        {
            _service = new WebClientFisProxyService(new EnlargeYourTimeoutClient(300000),
                new Uri("http://10.0.3.1:8080/import/"));
            _proxy = new FisProxy<TInput, TError, TReturn>(_service);
            //var filename = "OutLogs/" + DateTime.Today.ToString("dd-MM-yy") + MethodName + "log.txt";
            //var file = File.Exists(filename) ? File.AppendText(filename) : File.CreateText(filename);
            //if (_proxy.LogWriter == null) _proxy.LogWriter = file;
        }

        protected void CommitToDb(UGTUDataDataContext mainCtx)
        {
            try
            {
                mainCtx.SubmitChanges();
            }
            catch (Exception e)
            {
                MakeLog(e.Message);
                MakeLog(e.InnerException?.Message);
                throw;
            }
        }

        protected void MakeLog(string message)
        {
            LogWriter.MakeLog(message);
        }

        protected BaseProxyMethod()
        {
            CreateFisProxy();
            CreateRootAuth();
        }

        protected FisProxy<TInput, TError, TReturn> proxy => _proxy;

        public Answer<TReturn, TError> ReadDataFromFile(string fileName)
        {
            var doc = XDocument.Load(fileName);
            var res = XElement.Parse(doc.Element("Dictionaries").ToString());
            return _proxy.DeserialisDeserializeFromXml(res);
        }

        protected virtual void SetAuth()
        {
            //var auth = Package.GetType().GetProperty("AuthData"); //взять объект авторизации
            //var authType = auth.GetType();
            //var login = authType.GetProperty("Login");
            //if ((login.PropertyType != typeof(string))) return;


            //authType.GetProperty("Login").SetValue(auth, Login, null);
            //authType.GetProperty("Pass").SetValue(auth, Password, null);

            //login.SetValue(Package.GetType().GetField("AuthData"), Login);
            //authType.GetProperty("Pass").SetValue(auth, Password);
        }

        protected void SavePackNumber(uint packId)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                var pack = new PackageImport()
                {
                    id = packId.ToString(),
                    ImportTime = DateTime.Now
                };
                mainCtx.PackageImports.InsertOnSubmit(pack);

                CommitToDb(mainCtx);
            }
        }

        /// <summary>
        /// Получить и записать в БД справочники из ФИС
        /// </summary>
        /*
        public List<InstitutionExportsInstitutionExportOrderOfAdmission> GetOrders()
        {          
            return _sDao.OrderOfAdmission();
        }
        */



        /// <summary>
        /// Разобрать XML-файл с ответом от ФИС
        /// </summary>
        /// <param name="fName"></param>
        /*
        public void ParseXmlAnswer(string fName)
        {
            var doc = XDocument.Load(fName);

            var res = XElement.Parse(doc.Element("ImportResultPackage").ToString());

            var checkRes = FisProxy.Deserialize<ImportResultPackage, TError>(res);
            ParseImportResult(checkRes.Result);
        }*/

        /// <summary>
        /// удалить из их системы все заявления, которые были экспортированы
        /// </summary>
        /*
        public void DeleteExportedApplications()
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

        } */

        /// <summary>
        /// Удалить из их системы конкретное заявление
        /// </summary>
        /// <param name="nCode">Код абитуриента, которого нужно удалить</param>
        /*
        public void DeleteDefiniteApplication(int nCode)
        {
            var delApp = new List<DataForDeleteApplication>();
            using (var mainCtx = new UGTUDataDataContext())
            {
                foreach (var abit in mainCtx.ABIT_postups.Where(x => (x.nCode == nCode)))
                {
                    delApp.Add(new DataForDeleteApplication()
                    {
                        ApplicationNumber = Convert.ToString(abit.nCode),
                        RegistrationDate = abit.dd_pod_zayav
                    });
                }
            }

            _sDao.DeleteApplications(delApp);     
        }

        /// <summary>
        /// удалить из их системы все СПО
        /// </summary>
        public void DeleteSPOApplications()
        {
            var delApp = new List<DataForDeleteApplication>();

            using (var mainCtx = new UGTUDataDataContext())
            {
                foreach (
                    var abitSPO in
                        mainCtx.Export_FB_journals.Where(
                            x =>
                                mainCtx.ABIT_postups.Where(
                                    y => y.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_direction == 5)
                                    .Select(z => z.nCode)
                                    .Contains(x.nCode)))
                {
                    delApp.Add(new DataForDeleteApplication
                    {
                        ApplicationNumber = Convert.ToString(abitSPO.nCode),
                        RegistrationDate = abitSPO.Registration_Date.Value
                    });
                }
            }

            _sDao.DeleteApplications(delApp);
        }

        /// <summary>
        /// удалить из ФИС все заявления по конкретному набору
        /// </summary>
        /// <param name="specNnRecord"></param>
        public void DeleteApplicationByNNRecord(int specNnRecord)
        {
            var delApp = new List<DataForDeleteApplication>();

            using (var mainCtx = new UGTUDataDataContext())
            {
                foreach (var abit in mainCtx.Export_FB_journals.Where(x => (x.Registration_Date != null)
                                                                            &&
                                                                            (mainCtx.ABIT_postups.Any(
                                                                                y =>
                                                                                    (y.ABIT_Diapazon_spec_fac
                                                                                        .Main_NNRecord_FB ==
                                                                                     specNnRecord) &&
                                                                                    (y.nCode == x.nCode)) ||
                                                                             (specNnRecord == 0))))
                {
                    delApp.Add(new DataForDeleteApplication()
                    {
                        ApplicationNumber = Convert.ToString(abit.nCode),
                        RegistrationDate = (DateTime) abit.Registration_Date,
                    });
                }
            }

            _sDao.DeleteApplications(delApp);
        }*/

        /// <summary>
        /// удалить из ФИС все приказы
        /// </summary>
        /// <param name="year">Год</param>
        //public void DeleteOrders(int year)
        //{
        //    var delOrders = new List<Application>();

        //    using (var mainCtx = new UGTUDataDataContext())
        //    {
        //        foreach (
        //            var abit in
        //                mainCtx.Export_FB_journals.Where(
        //                    x => (x.Registration_Date != null) && (x.Registration_Date.Value.Year == year)))
        //        {

        //            if (mainCtx.ABIT_postups.Any(y => (y.nCode == abit.nCode) && (y.ik_prikaz_zach != null)
        //                /*&&(y.ABIT_Diapazon_spec_fac.Main_NNRecord_FB == 1777)*/))
        //            {
        //                delOrders.Add(new Application()
        //                {
        //                    ApplicationNumber = abit.nCode.ToString(),
        //                    RegistrationDate = (DateTime) abit.Registration_Date,
        //                    NeedHostelSpecified = false,
        //                    StatusIDSpecified = false
        //                });
        //            }
        //        }
        //    }

        //    _sDao.DeleteOrders(delOrders);
        //} 


        /// <summary>
        ///     Производит экспорт в ФИС ЕГЭ данных заданного абитуриента
        /// </summary>
        /// <param name="exportParam">Параметры абитуриента для экспорта</param>
        /// <param name="year">Год</param>
        //internal void ExportSingle(int nCode, int year)
        //{
        //    var exportParam = new ExportParam(ExportType.Single, nCode);
        //    Contract.Requires(exportParam.ExportType == ExportType.Single);
        //    using (var mainCtx = new UGTUDataDataContext())
        //    {
        //        var application =
        //            Collector.BuildApplicationPackage(mainCtx.Persons.Single(x => x.nCode == exportParam.AbitId),
        //                mainCtx, year);
        //        Contract.Assert(application != null);
        //        _sDao.ExportSingle(application);
        //    }
        //}

        /// <summary>
        /// экспорт всех новых абитуриентов: текущего года, с определенным состоянием, которых нет в журнале или потеряли актуальность
        /// </summary>
        /// <param name="year">Год</param>
        //public void ExportApplications(int year)
        //{
        //    using (var mainCtx = new UGTUDataDataContext())
        //    {
        //        var pack = new PackageData
        //        {
        //            Applications = Collector.GetCurrentApps(mainCtx, year, 0, 0).ToList()
        //        };
        //        CommitToDb(mainCtx);

        //        RepeatExportTillResult(pack);
        //    }
        //}



        /// <summary>
        /// После выполненного импорта начинаем запрашивать результаты импорта пакета
        /// </summary>
        /// <param name = "pakId" > Номер пакета</param>
        //private void GetImportResultById(uint pakId)
        //{
        //    _sDao.MakeLog("Приступаем к ImportPackResult");
        //    var tryCount = 0;
        //    ImportResultPackage checkRes;
        //    try
        //    {
        //        checkRes = _sDao.GetExportPackResult(pakId);
        //        if (checkRes == null)
        //        {
        //            _sDao.MakeLog("checkRes.Result == null");
        //        }
        //        _sDao.MakeLog("ImportPackResult прошел");

        //        while ((checkRes == null) && (tryCount < 20)) //по номеру получить результат экспорта
        //        {
        //            _sDao.MakeLog("Пока не импортировано");
        //            _sDao.MakeLog("Приступаем к сну");
        //            Thread.Sleep(100000); //запрашиваем в цикле и ждем, ибо у них на месте пакеты обрабатываются долго
        //            checkRes = _sDao.GetExportPackResult(pakId);
        //            tryCount++;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        _sDao.MakeLog(exception.Message);
        //        _sDao.MakeLog("SubMetod");
        //        return;
        //    }

        //    if (checkRes == null) return;

        //    ParseImportResult(checkRes);
        //}

        /// <summary>
        /// Распаковать и записвать ошибки по импорту заявлений и приказов
        /// </summary>
        /// <param name="checkRes">Пакет</param>
        //    private void ParseImportResult(ImportResultPackage checkRes)
        //    {
        //        using (var mainCtx = new UGTUDataDataContext())
        //        {
        //            //ошибки по приказам
        //            if (checkRes.Log.Failed.OrdersOfAdmissions != null)
        //            {
        //                foreach (var badApp in checkRes.Log.Failed.OrdersOfAdmissions)
        //                {
        //                    var jourRecord =
        //                        mainCtx.Export_FB_journals.FirstOrDefault(
        //                            x => x.nCode.ToString() == badApp.ApplicationNumber);
        //                    jourRecord.Prikaz_result = badApp.ErrorInfo.Message;

        //                    jourRecord.ErrorCode = (int?) badApp.ErrorInfo.ErrorCode;
        //                    jourRecord.Is_actual = false;
        //                }
        //            }

        //            //ошибки по импорту заявлений
        //            if (checkRes.Log.Failed.Applications != null)
        //            {
        //                foreach (var badApp in checkRes.Log.Failed.Applications)
        //                {
        //                    var jourRecord =
        //                        mainCtx.Export_FB_journals.FirstOrDefault(
        //                            x => x.nCode.ToString() == badApp.ApplicationNumber);
        //                    jourRecord.Import_result = badApp.ErrorInfo.Message;

        //                    jourRecord.ErrorCode = (int?) badApp.ErrorInfo.ErrorCode;
        //                    jourRecord.Is_actual = false;
        //                }
        //            }
        //            CommitToDb(mainCtx);
        //        }
        //    }
    }
}
