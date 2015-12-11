using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fdalilib;
using Fdalilib.Service;
using Fdalilib.ImportClasses;
//using Fdalilib.XMLCODE;
//using Fdalilib.XMLCODE.Applications;
//using Fdalilib.XMLCODE.Errors;
//using Fdalilib.XMLCODE.Logs;

namespace AbitExportProject
{
    /// <summary>
    /// Класс доступа к сервису.
    /// </summary>
    class AbitServiceDao
    {
        private static volatile AbitServiceDao instance;
        private static object syncRoot = new Object();

        private FisProxy proxy;

        private TError _lastError=null;

        private bool _isLastFail = false;

        /// <summary>
        /// Возвращает значение при успешном ответе сервера на запрос или null и устанавливает поле последней ошибки и флаг ошибки.
        /// </summary>
        /// <typeparam name="TReturn">Тип возвращаемого в случае успешного завершения операции ответа.</typeparam>
        /// <typeparam name="TInput">Тип передаваемый в метод в контейнере Answer.</typeparam>
        /// <param name="answer">Ответ сервера в контейнере Answer.</param>
        /// <param name="func">Функция получения нужного значения из ответа сервера.</param>
        /// <returns></returns>
        private TReturn ReturnOrNullAndError<TReturn,TInput>(Answer<TInput, TError> answer, Func<TInput,TReturn> func)
            where TReturn : class
            where TInput : class
        {
            if (answer.IsSucceded)
            {
                _isLastFail = false;
                return func(answer.Result);
            }
            else
            {
                _isLastFail = true;
                _lastError = answer.Error;
                return null;
            }
        } 

        public bool IsLastFail
        {
            get { return _isLastFail; }
        }

        public TError LastError
        {
            get { return _lastError; }
        }

        private AbitServiceDao()
        {
            //WebRequest.DefaultWebProxy = new WebProxy("http://195.22.104.27:3128/", true);
            IFisProxyService service = new WebClientFisProxyService(new EnlargeYourTimeoutClient(300000), new Uri("http://10.0.3.1:8080/import/"));
            proxy = new FisProxy("pk@ugtu.net", "2lvFeaJ", service)
            {
                LogWriter =
                    File.CreateText("OutLogs/" + ((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds) + "log.txt")
            };
        }

        public static AbitServiceDao Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        return new AbitServiceDao();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Экспорт одного заявления
        /// </summary>
        /// <param name="application">заявление</param>
        /// <returns>результат импорта</returns>
        /// TODO: решить, что же возвращать, потому как так нельзя, товарищи.
        public ImportResultPackage ExportSingle(Application application)
        {
            return ReturnOrNullAndError<ImportResultPackage, ImportResultPackage>(proxy.ImportSingle(application), x => x);
        }

        //public ExportBatch

        /// <summary>
        /// Получение списка экспортированных заявлений
        /// </summary>
        /// <returns>список экспортированных заявлений</returns>
        public List<InstitutionExportsInstitutionExportApplication> GetExportedApps()
        {
            return ReturnOrNullAndError(proxy.GetUniversityInfo(), x => x.InstitutionExport.Applications);
        }

        public List<InstitutionExportsInstitutionExportOrderOfAdmission> OrderOfAdmission()
        {
            return ReturnOrNullAndError(proxy.GetUniversityInfo(), x => x.InstitutionExport.OrdersOfAdmission);
        }

        /// <summary>
        /// Удаление списка заявлений
        /// </summary>
        /// <param name="apps">Список заявлений</param>
        /// <returns>Идентификатор пакета операции</returns>
        /// А вот эти скачки с типами, это потому что иерархия не от Object для value types.
        public uint? DeleteApplications(List<DataForDeleteApplication> apps)
        {
            var dataForDelete = new DataForDelete() {Applications = apps};
            return (uint?)ReturnOrNullAndError<Object, DeletePackageInfo>(proxy.DeleteData(dataForDelete),
                                                                          x => (Object)x.PackageID); 
        }

        public uint? DeleteOrders(List<Application> apps)
        {
            var dataForDelete = new DataForDelete() { OrdersOfAdmission = apps };
            return (uint?)ReturnOrNullAndError<Object, DeletePackageInfo>(proxy.DeleteData(dataForDelete),
                                                                          x => (Object)x.PackageID);
        }

    }


}
