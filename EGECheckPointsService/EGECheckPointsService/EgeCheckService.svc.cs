using System;
using System.Net;
using EGECheckPointsService.FCTServiceReference;

namespace EGECheckPointsService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class EgeCheckService : IEgeCheckService
    {
        readonly FCTServiceReference.WSChecksSoapClient _service = new WSChecksSoapClient();
        private readonly UserCredentials _credentials;
        
        /// <summary>
        /// Создаёт экземпляр службы проверки сертификата ЕГЭ.
        /// Внимание! Служба использует специально настроенный прокси для установки связи с сервисом федеральной информационной системы
        /// </summary>
        EgeCheckService()
        {
            WebRequest.DefaultWebProxy = new WebProxy("http://195.22.104.27:3128/", true);
            _credentials = new UserCredentials { Login = "fmarakasov@ugtu.net", Password = "bylnMu4" };
        }

        public string GetSingleCheckQuerySample()
        {
            return _service.GetBatchCheckQuerySample();
        }

        public string GetBatchCheckQuerySample()
        {
            return _service.GetBatchCheckQuerySample();
        }

        /// <summary>
        /// Возвращает результат проверки сертификата ЕГЭ для заданного абитуриента. Обязательными параметрами являются ФИО, серия и номер паспорта. 
        /// Если указаны номер и типографский номер сертификата, то возвращаются данные указанного сертификата, в противном случае 
        /// возвращаются данные о всех сертификатах абитуриента.
        /// </summary>
        /// <param name="lastName">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="patronymicName">Отчество</param>
        /// <param name="passportSeria">Серия паспорта</param>
        /// <param name="passportNumber">Номер паспорта</param>
        /// <param name="certificateNumber">Номер сертификата</param>
        /// <param name="typographicNumber">Типографский номер сертификата</param>
        /// <returns>Строка с разметной XML с данными о сертификате</returns>
        public string SingleCheck(string lastName, string firstName, string patronymicName, string passportSeria, string passportNumber, string certificateNumber, string typographicNumber)
        {
            try
            {
                string xmlOutput = string.Format(Resource.XmlQuery, lastName, firstName, patronymicName, passportSeria,
                                                 passportNumber, certificateNumber, typographicNumber);
                return _service.SingleCheck(_credentials, xmlOutput);
            }
            catch (Exception exception)
            {
                return string.Format(Resource.XmlError,exception.Message);
            }
        }


        public string SingleCheckTest(string lastname, string firstname, string patronymicname, string passportseria, string passportnumber, string certificatenumber, string typographicnumber)
        {
            if(!string.IsNullOrWhiteSpace(lastname) && !string.IsNullOrWhiteSpace(firstname) && !string.IsNullOrWhiteSpace(patronymicname) && !string.IsNullOrWhiteSpace(passportnumber) && !string.IsNullOrWhiteSpace(passportseria))
                return string.Format(Resource.XmlResulTemplate, lastname, firstname, patronymicname, passportseria,
                                                 passportnumber, certificatenumber, typographicnumber);
            return string.Format(Resource.XmlError, "Не верно указаны атрибуты");
        }

        //public string BatchCheck()
        //{
        //    throw new NotImplementedException();
        //}

        //public string GetBatchCheckResult()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
