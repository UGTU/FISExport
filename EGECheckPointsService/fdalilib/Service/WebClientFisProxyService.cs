using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Fdalilib.Service
{
    /// <summary>
    ///     Реализует доступ к службе интеграции с федеральной системой приёма заявлений с использованием http протокола
    /// </summary>
    public class WebClientFisProxyService : IFisProxyService
    {
        /// <summary>
        ///     Создаёт экземпляр WebClientIntegrationService. В качестве параметров передаются объект WebClient, который будет использован для доступа к методам службы и
        ///     Uri с указанием базового адреса методов службы (на текущий момент базовый адрес службы задан как http://10.0.3.1:8080/import/)
        /// </summary>
        /// <param name="client">Экземпляр WebClient для доступа к службе интеграции по http протоколу</param>
        /// <param name="baseUri">Базовый адрес службы</param>
        public WebClientFisProxyService(WebClient client, Uri baseUri)
        {
            Contract.Requires(client != null);
            Contract.Requires(baseUri!=null);
            Client = client;
            BaseUri = baseUri;
        }

        /// <summary>
        ///     Получает экземпляр WebClient, используемый для доступа к службе интеграции
        /// </summary>
        public WebClient Client { get; private set; }

        /// <summary>
        ///     Получает базовый URI методов службы интеграции
        /// </summary>
        public Uri BaseUri { get; private set; }



        /// <summary>
        ///     Получает коллекцию справочников
        /// </summary>
        /// <param name="authData">Данные аутентификации</param>
        /// <returns>Коллекция имён справочников</returns>
        public XElement GetDictionaries(XElement authData)
        {
            return DoQuery(authData, "importservice.svc/dictionary");
        }

        /// <summary>
        ///     Получает содержимое справочника
        /// </summary>
        /// <param name="dictionaryContent">Параметры запроса: аутенитфикационные данные и идентификатор справочника</param>
        /// <returns>Содержимое заданного справочника</returns>
        public XElement GetDictionaryDetails(XElement dictionaryContent)
        {
            return DoQuery(dictionaryContent, "importservice.svc/dictionarydetails");
        }

        /// <summary>
        ///     Получает результат проверки заданного заявления
        /// </summary>
        /// <param name="xElement">Параметры запроса: аутентификационные данные, номер и дата регистрации заявления</param>
        /// <returns></returns>
        public XElement GetSingleCheckApp(XElement xElement)
        {
            return DoQuery(xElement, "importservice.svc/checkapplication/single");
        }

        /// <summary>
        ///     Получает результат проверки пакета заявлений
        /// </summary>
        /// <param name="xElement">Параметры запроса: аутентификационные данные, идентификатор пакета заявлений</param>
        /// <returns></returns>
        public XElement GetCheckPack(XElement xElement)
        {
            return DoQuery(xElement, "importservice.svc/checkapplication");
        }

        /// <summary>
        ///  Импортирует одно заявление абитуриента
        /// </summary>
        /// <param name="xElement">Данные заявления</param>
        /// <returns>Результат импорта</returns>
        public XElement ImportSingle(XElement xElement)
        {
            return DoQuery(xElement, "importservice.svc/import/application/single");
        }

        /// <summary>
        ///  Валидация одного заявление абитуриента
        /// </summary>
        /// <param name="xElement">Данные заявления</param>
        /// <returns>Результат импорта</returns>
        public XElement ValidatePack(XElement element)
        {
            return DoQuery(element, "importservice.svc/validate");
        }

        /// <summary>
        ///  Валидация одного заявление абитуриента
        /// </summary>
        /// <param name="xElement">Данные заявления</param>
        /// <returns>Результат импорта</returns>
        public XElement ImportPack(XElement element)
        {
            return DoQuery(element, "importservice.svc/import");
        }

        /// <summary>
        /// Получение результата импорта пакета заявлений абитуриента
        /// </summary>
        /// <param name="xElement">Пакет заявления</param>
        /// <returns>Результат импорта</returns>
        public XElement ImportPackResult(XElement element)
        {
            return DoQuery(element, "importservice.svc/import/result");
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="xElement">Информация для удаления</param>
        /// <returns>Результат удаления</returns>
        public XElement DeleteData(XElement element)
        {
            return DoQuery(element, "importservice.svc/delete");
        }

        // <summary>
        /// Результат удаления 
        /// </summary>
        /// <param name="xElement">Пакет, попытка удаления которого была произведена</param>
        /// <returns>Результат удаления с конфликтами</returns>
        public XElement DeleteDataResult(XElement element)
        {
            return DoQuery(element, "importservice.svc/delete/result");
        }

        public XElement GetUniversityInfo(XElement xElement)
        {
            return DoQuery(xElement, "importservice.svc/institutioninfo");
        }

        /// <summary>
        ///     Выполняет запрос заданного метода службы
        /// </summary>
        /// <param name="element">Параметры метода</param>
        /// <param name="relativeUri">Относительный адрес Uri, идентифицирующий метод службы</param>
        /// <returns>Результат вызова службы</returns>
        private XElement DoQuery(XElement element, string relativeUri)
        {
            Contract.Requires(element!=null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            var uri = GetUri(relativeUri);
            Contract.Assert(uri != null);
            string innerParam = element.ToString();

            /*var file1 = new System.IO.StreamWriter("requerst.txt");
            file1.WriteLine(innerParam);
            file1.Close();*/           

            Client.Encoding = Encoding.UTF8;
            Client.Headers["Content-Type"] = "text/xml";

            string result = "";
            try
            {
                result = Client.UploadString(uri, innerParam);
                Contract.Assert(!string.IsNullOrWhiteSpace(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new XElement(XName.Get("NULL"));
            }

            /*var file = new System.IO.StreamWriter("result.txt");
            file.WriteLine(result);
            file.Close();*/

            return XElement.Load(new StringReader(result));
        }


        /// <summary>
        ///     Получает абсолютный Uri
        /// </summary>
        /// <param name="relative">Относительный адрес</param>
        /// <returns>Абсолютный адрес в объектк типа Uri</returns>
        private Uri GetUri(string relative)
        {
            return new Uri(BaseUri, relative);
        }
    }
}