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
//using Fdalilib.Actions.BatchApplicationImport;
//using Fdalilib.ImportClasses;
//using Fdalilib.XMLCODE;
//using Fdalilib.XMLCODE.Applications;
//using Fdalilib.XMLCODE.Errors;
//using Fdalilib.XMLCODE.Logs;

namespace AbitExportProject
{
    /// <summary>
    /// Класс доступа к сервису.
    /// </summary>
    //public class AbitServiceDao
    //{
    //  //  private static volatile AbitServiceDao instance;
    //   // private static object syncRoot = new Object();

    //   // private FisProxy proxy;

    //    //private TError _lastError=null;

    //    //private bool _isLastFail = false;

 

    //    //public bool IsLastFail
    //    //{
    //    //    get { return _isLastFail; }
    //    //}

    //    //public TError LastError
    //    //{
    //    //    get { return _lastError; }
    //    //}

    //    private AbitServiceDao()
    //    {
    //        //WebRequest.DefaultWebProxy = new WebProxy("http://195.22.104.27:3128/", true);
            
    //        proxy = FisProxy.Instance("pk@ugtu.net", "2lvFeaJ", service);
    //        proxy.
    //    }

    //    public TextWriter Logger => proxy.LogWriter;

    //    public static AbitServiceDao Instance
    //    {
    //        get
    //        {
    //            if (instance != null) return instance;
    //            lock (syncRoot)
    //            {
    //                instance = new AbitServiceDao();
    //            }
    //            return instance;
    //        }
    //    }

    //    public void MakeLog(string message)
    //    {
    //        proxy.LogWriter.Write(message);
    //        proxy.LogWriter.Flush();
    //    }

    //    /// <summary>
    //    /// Экспорт одного заявления
    //    /// </summary>
    //    /// <param name="application">заявление</param>
    //    /// <returns>результат импорта</returns>
    //    /// TODO: решить, что же возвращать, потому как так нельзя, товарищи.
    //    public ImportResultPackage ExportSingle(Application application)
    //    {
    //        return ReturnOrNullAndError<ImportResultPackage, ImportResultPackage>(proxy.ImportSingle(application), x => x);
    //    }

    //    /// <summary>
    //    /// Отослать в ФИС пакет с данными
    //    /// </summary>
    //    /// <param name="package">пакет</param>
    //    /// <returns></returns>
    //    public ImportPackageInfo ExportBatch(PackageData package)
    //    {
    //        return ReturnOrNullAndError(proxy.ImportPack(package), x => x);
    //    }

    //    /// <summary>
    //    /// Получить результат экспорта по ID
    //    /// </summary>
    //    /// <param name="packageId"></param>
    //    /// <returns></returns>
    //    public ImportResultPackage GetExportPackResult(uint packageId)
    //    {
    //        return ReturnOrNullAndError(proxy.ImportPackResult(packageId), x => x);
    //    }

    //    /// <summary>
    //    /// Получение списка экспортированных заявлений
    //    /// </summary>
    //    /// <returns>список экспортированных заявлений</returns>
    //    public List<InstitutionExportsInstitutionExportApplication> GetExportedApps()
    //    {
    //        return ReturnOrNullAndError(proxy.GetUniversityInfo(), x => x.InstitutionExport.Applications);
    //    }

    //    /// <summary>
    //    /// Получение импортированных приказов
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<InstitutionExportsInstitutionExportOrderOfAdmission> OrderOfAdmission()
    //    {
    //        return ReturnOrNullAndError(proxy.GetUniversityInfo(), x => x.InstitutionExport.OrdersOfAdmission);
    //    }

    //    /// <summary>
    //    /// Получение списка справочников
    //    /// </summary>
    //    /// <returns></returns>
    //    public Dictionaries Dictionaries() => ReturnOrNullAndError(proxy.GetDictionaries(), x => x);

    //    /// <summary>
    //    /// Получение деталей справочника
    //    /// </summary>
    //    /// <param name="id">Код справочника</param>
    //    /// <returns></returns>
    //    public DictionaryData DictionaryDetails(uint id) => ReturnOrNullAndError(proxy.GetDictionaryDetails(id), x => x);

    //    /// <summary>
    //    /// Удаление списка заявлений
    //    /// </summary>
    //    /// <param name="apps">Список заявлений</param>
    //    /// <returns>Идентификатор пакета операции</returns>
    //    /// А вот эти скачки с типами, это потому что иерархия не от Object для value types.
    //    public uint? DeleteApplications(List<DataForDeleteApplication> apps)
    //    {
    //        var dataForDelete = new DataForDelete() {Applications = apps};
    //        return (uint?)ReturnOrNullAndError<Object, DeletePackageInfo>(proxy.DeleteData(dataForDelete),
    //                                                                      x => (Object)x.PackageID); 
    //    }

    //    public uint? DeleteOrders(List<Application> apps)
    //    {
    //        var dataForDelete = new DataForDelete() { OrdersOfAdmission = apps };
    //        return (uint?)ReturnOrNullAndError<Object, DeletePackageInfo>(proxy.DeleteData(dataForDelete),
    //                                                                      x => (Object)x.PackageID);
    //    }

    //}


}
