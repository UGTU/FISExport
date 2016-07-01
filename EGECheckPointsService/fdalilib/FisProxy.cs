using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Fdalilib.Service;
//using Fdalilib.ImportClasses;
//using Fdalilib.XMLCODE;
//using Fdalilib.XMLCODE.Applications;
//using Fdalilib.XMLCODE.CheckResults;
//using Fdalilib.XMLCODE.Dictionaries;
//using Fdalilib.XMLCODE.Errors;
//using Fdalilib.XMLCODE.Logs;

namespace Fdalilib
{
    /// <summary>
    ///     Класс предоставляет методы для интеграции с сервисами федеральной базы данных абитуриентов (ФИС ЕГЭ)
    ///     Взаимодейcтсвие обеспечивается на основе передачи параметров и возврата результатов в виде объектов
    ///     классов, полученных на основе описания XSD, вместо манипулирования типами XNode
    /// </summary>
    /// 
    public sealed class FisProxy<TInput,TError,TReturn>  where TInput : class where TError : class where TReturn : class
    {
        private TError _lastError = null;

        private bool _isLastFail = false;
       

       // private static FisProxy<TPackage, TError> _instance;

      /*  public static FisProxy<TPackage, TError> Instance(IFisProxyService service)
        {

                if (_instance != null) return _instance;
                lock (_syncRoot)
                {
                _instance = new FisProxy<TPackage, TError>(service);
                }
                return _instance;
        }*/

        /// <summary>
        /// Возвращает значение при успешном ответе сервера на запрос или null и устанавливает поле последней ошибки и флаг ошибки.
        /// </summary>
        /// <typeparam name="TReturn">Тип возвращаемого в случае успешного завершения операции ответа.</typeparam>
        /// <typeparam name="TInput">Тип передаваемый в метод в контейнере Answer.</typeparam>
        /// <typeparam name="TError">Тип ошибки, возвращаемой в результате запроса</typeparam>
        /// <param name="answer">Ответ сервера в контейнере Answer.</param>
        /// <param name="func">Функция получения нужного значения из ответа сервера.</param>
        /// <param name="package">Пакет с данными</param>
        /// <param name="methodName">Имя вызываемого метода</param>
        /// <returns></returns>
        public TReturn ReturnOrNullAndError(TInput package, string methodName)
        {
           // var getName = Service.GetType().GetMethod(methodName);
            //var func = /*(Func<XElement, XElement>)*/Delegate.(typeof(Func<XElement, XElement>), getName);
            var func = (Func<XElement, XElement>)Delegate.CreateDelegate(typeof(Func<XElement, XElement>), Service, methodName);
            var answer = Request<TReturn, TError>(func, package);
            if (answer.IsSucceded)
            {
                _isLastFail = false;
                return answer.Result;
            }
            else
            {
                _isLastFail = true;
                _lastError = answer.Error;
                return null;
            }
        }
        /*
        public TReturn GetResultByMethod<TReturn>(XElement method, TPackage pack)
        {
            return ReturnOrNullAndError(Request<TReturn>(method, pack));
        }*/

        /// <summary>
        ///     Создаёт новый экземпляр класса интеграции
        ///     Принимает в качестве параметра объект аутентификации для коммуникации с сервисом
        /// </summary>
        /// <param name="service">Сервис федеральной системы абитуриентов</param>
        public FisProxy(IFisProxyService service)
        {
            Contract.Requires(service != null);
            Service = service;
            
        }


        /// <summary>
        ///     Получает объект, реализующий интерфейс IIntegrationService, который используется для связи с сервисом интеграции федеральной системы приёма заявлений
        /// </summary>
        public IFisProxyService Service { get; private set; }

        /// <summary>
        /// Выполняет запрос к ФИС ЕГЭ. Принимает в качестве параметра функцию сервиса и объект с данными запроса к сервису, 
        /// возвращает результат в виде объекта типа Answer с заданными типами T и TErr
        /// </summary>
        /// <typeparam name="T">Тип результата при удачном вызове метода сервиса</typeparam>
        /// <typeparam name="TErr">Тип результата, если вызов метода сервиса вернул ошибку</typeparam>
        /// <param name="func">Метод сервиса для вызова</param>
        /// <param name="param">Объект с параметрами запрса</param>
        /// <returns>Результат вызова сервиса в объекте типа Answer</returns>
        private Answer<T, TErr> Request<T, TErr>(Func<XElement, XElement> func, TInput param) where T : class where TErr : class
        {
            Contract.Requires(func != null);
            Contract.Requires(param != null);
            var xParam = Serialize(param);
            LogWriter.MakeLog(xParam.ToString());
            var result = func(xParam);
            Contract.Assert(result != null);
            LogWriter.MakeLog(result.ToString());
            LogWriter.MakeLog("----------------------------\\---------------------------------------------");
            return Deserialize<T, TErr>(result);
        }

        public Answer<TReturn, TError> DeserialisDeserializeFromXml(XElement result)
        {
            return Deserialize<TReturn, TError>(result);
        }

        /// <summary>
        ///     Сериализует заданный объект в объект типа XElement
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Сериализованный объект в XElement</returns>
        public static XElement Serialize(object obj)
        {
            Contract.Requires(obj!=null);
            Contract.Ensures(Contract.Result<XElement>()!=null);
            var mem = new MemoryStream();
            var ser = new XmlSerializer(obj.GetType());
            ser.Serialize(mem, obj);
            mem.Position = 0;
            return XElement.Load(mem);
            
        }

        /*    private static XElement DirtyHackToFixLocale(XElement element)
            {
                //dirty hack to fix DateTime
                Console.WriteLine(element.ToString());

                var fixDateElements = element.Descendants("DocumentDate").ToList();
                fixDateElements.AddRange(element.Descendants("BirthDate").ToList());
                fixDateElements.AddRange(element.Descendants("DictionaryDate").ToList());
               // fixDateElements.AddRange(element.Descendants("DateOrder").ToList());
               // FixDateElements.AddRange(element.Descendants("LicenseDate").ToList());
                //FixDateElements.AddRange(element.Descendants("Accreditation").ToList());
                foreach (var elm in fixDateElements)
                {
                    try
                    {
                        var date = DateTime.ParseExact(elm.Value, "dd.MM.yyyy h:mm:ss", CultureInfo.CurrentCulture);
                        elm.ReplaceAll(date.ToString("yyyy-MM-dd"));
                    }
                    catch (Exception e)
                    {
                    }
                }

                //end of dirty hack

                //dirty hack to fix floats

                var fixValueElements = element.Descendants("Value").ToList();
                foreach (var elm in fixValueElements)
                {
                    try
                    {
                        var val = Double.Parse(elm.Value, CultureInfo.CreateSpecificCulture("ru-RU"));
                        elm.ReplaceAll(val);
                    }
                    catch (Exception e)
                    {
                    }
                }

                //end of dirty hack

                return element;
            }*/

        /// <summary>
        ///     Десереализует заданный объект из типа XNode в заданный тип
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <typeparam name="TErr">Тип для описания ошибки</typeparam>
        /// <param name="element">Сериализованный объект в XNode</param>
        /// <returns>Восстановленный объект заданного типа в составе объекта Answer</returns>
        public static Answer<T, TErr> Deserialize<T, TErr>(XElement element)
            where T : class
            where TErr : class
        {
            Contract.Requires(element != null);

            //  element = DirtyHackToFixLocale(element);

            var reader = element.CreateReader();

            Type expectedType = null;
            if (IsSpecifiedElement<T>(element))
                expectedType = typeof(T);
            else
                if (IsSpecifiedElement<TErr>(element))
                expectedType = typeof(TErr);

            if (expectedType == null) return new Answer<T, TErr>(null);
            var ser = new XmlSerializer(expectedType);
            var obj = ser.Deserialize(reader);
            return new Answer<T, TErr>(obj);
        }

        /// <summary>
        ///     Получает призак того, что элемент XML содержит сереализованный экземпляр объекта заданного типа
        /// </summary>
        /// <typeparam name="T">Тип класса ошибок</typeparam>
        /// <param name="element">Элемент</param>
        /// <returns>Истина, если элемент содержит описание типа T</returns>
        private static bool IsSpecifiedElement<T>(XElement element) where T : class
        {
            Contract.Requires(element != null);
            var type = typeof (T);
            var attr = type.GetCustomAttributes(typeof (XmlRootAttribute), true);
            if (attr.Length == 1)
            {
                var root = attr[0] as XmlRootAttribute;
                Contract.Assert(root != null, "root != null");
                if (!string.IsNullOrEmpty(root.ElementName))
                    return (root.ElementName == element.Name);
            }
            return type.Name == element.Name;
        }

        public bool IsLastFail => _isLastFail;

        public TError LastError => _lastError;

        ///// <summary>
        ///// Создаёт экземпляр корнегого элемента с данными аутентификации, переданными в параметрах конструктора, для использования в качестве параметра к сервису ФИС ЕГЭ
        ///// </summary>
        ///// <returns>Экземпляр Root </returns>
        //private Root CreateRootAuth()
        //{
        //    return new Root {AuthData = new AuthData()};
        //}

        /// <summary>
        ///     Получает перечень справочников
        /// </summary>
        /// <returns>Перечень справочников из федеральной системы абитуриентов</returns>
        //public Answer<Dictionaries, TError> GetDictionaries()
        //{
        //    return Request<Dictionaries, TError>(Service.GetDictionaries, CreateRootAuth());
        //}

        ///// <summary>
        /////     Получает содержимое заданного справочника
        ///// </summary>
        ///// <param name="code">Идентификатор справочника</param>
        ///// <returns>Содержимое справочника</returns>
        //public Answer<DictionaryData, TError> GetDictionaryDetails(uint code)
        //{
        //    var root = CreateRootAuth();
        //    root.GetDictionaryContent = new GetDictionaryContent {DictionaryCode = (uint) code};
        //    return Request<DictionaryData, TError>(Service.GetDictionaryDetails, root);
        //}

        ///// <summary>
        /////     Получает результат проверки одного заявления
        ///// </summary>
        ///// <param name="appNumber">Номер заявления</param>
        ///// <param name="registrationDate">Дата регистрации</param>
        ///// <returns>Результат проверки заявления</returns>
        //public Answer<AppSingleCheckResult, TError> GetSingleCheckApp(string appNumber, DateTime registrationDate)
        //{
        //    var root = CreateRootAuth();
        //    root.CheckApp = new CheckApp {ApplicationNumber = appNumber, RegistrationDate = registrationDate};
        //    return Request<AppSingleCheckResult, TError>(Service.GetSingleCheckApp, root);                        
        //}

        ///// <summary>
        /////     Импорт одного заявления абитуриента
        ///// </summary>
        ///// <param name="application">Данные по заявлению</param>
        ///// <returns>Результат импорта заявления</returns>
        //public Answer<ImportResultPackage, TError> ImportSingle(Application application)
        //{
        //    var root = CreateRootAuth();
        //    root.PackageData = new PackageData { Applications = new List<Application>()};
        //    root.PackageData.Applications.Add(application);
        //    return Request<ImportResultPackage, TError>(Service.ImportSingle, root);
        //}

        ////TODO: выяснить что еще в PackageData для валидации за поля. 
        ///// <summary>
        /////     Валидация пакета заявлений
        ///// </summary>
        ///// <param name="package">Пакет с данными по импорту</param>
        ///// <returns>Результат валидации заявления. Код ошибок см. в справочнике.</returns>
        //public Answer<ValidationResultPackage, TError> ValidatePack(PackageData package)
        //{
        //    var root = CreateRootAuth();
        //    root.PackageData = package;
        //    return Request<ValidationResultPackage, TError>(Service.ValidatePack, root);
        //}

        ////TODO: проверить результат импорта
        ///// <summary>
        /////     Импорт пакета заявлений
        ///// </summary>
        ///// <param name="package">Пакет с данными по импорту</param>
        ///// <returns>Результат импорта заявления. Код ошибок см. в справочнике.</returns>
        //public Answer<ImportPackageInfo, TError> ImportPack(PackageData package)
        //{
        //    var root = CreateRootAuth();
        //    root.PackageData = package;
        //    return Request<ImportPackageInfo, TError>(Service.ImportPack, root);
        //}

        ///// <summary>
        /////     Получение результата импорта пакета заявлений
        ///// </summary>
        ///// <returns>Результат импорта заявления с конфликтами.</returns>
        //public Answer<ImportResultPackage, TError> ImportPackResult(uint packageId)
        //{
        //    var root = CreateRootAuth();
        //    root.GetResultImportApplication = new GetResultImportApplication() { PackageID =  packageId };
        //    return Request<ImportResultPackage, TError>(Service.ImportPackResult, root);
        //}

        ///// <summary>
        /////    Удаление данных
        ///// </summary>
        ///// <param name="dataForDelete">Объект с описанием данных для удаления из ФИС ЕГЭ</param>
        ///// <returns>Результат удаления с номером пакета.</returns>
        //public Answer<DeletePackageInfo, TError> DeleteData(DataForDelete dataForDelete)
        //{
        //    var root = CreateRootAuth();
        //    root.DataForDelete = dataForDelete;
        //    return Request<DeletePackageInfo, TError>(Service.DeleteData, root);
        //}

        ///// <summary>
        /////    Получение результата удаления
        ///// </summary>
        ///// <param name="packageId">Идентификатор пакета с удаляемыми данными</param>
        ///// <returns>Результат удаления с конфликтами.</returns>
        ////DeleteResultPackage
        //public Answer<DeleteResultPackage, TError> DeleteDataResult(uint packageId)
        //{
        //    var root = CreateRootAuth();
        //    root.GetResultDeleteApplication = new GetResultDeleteApplication { Item =  packageId}; //PackageID =
        //    return Request<DeleteResultPackage, TError>(Service.DeleteDataResult, root);
        //}

        ///// <summary>
        /////     Получает результат проверки импорта пакета
        ///// </summary>
        ///// <param name="packageId">Идентификатор импортированного пакета</param>
        ///// <returns>Результат проверки заявления</returns>
        //public Answer<ImportResultPackage, TError> GetCheckPack(uint packageId)
        //{
        //    var root = CreateRootAuth();
        //    root.GetResultCheckApplication = new GetResultCheckApplication { Item = packageId};
        //    return Request<ImportResultPackage, TError>(Service.GetCheckPack, root);
        //}

        ///// <summary>
        /////     Возвращает информацио об образовательном учреждении
        ///// </summary>
        ///// <param name="appNumber">Номер учреждения(опционально)</param>
        ///// <returns>Информация об учреждении</returns>
        //public Answer<InstitutionExports, TError> GetUniversityInfo(int? universityID = null)
        //{
        //    var root = CreateRootAuth();
        //    if (universityID.HasValue)
        //    {
        //        root.AuthData.InstitutionID = universityID.Value;
        //    }
        //    return Request<InstitutionExports, TError>(Service.GetUniversityInfo, root);
        //}
    }
}