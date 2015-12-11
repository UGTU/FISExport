using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EGECheckPointsService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IEgeCheckService
    {

        /// <summary>
        /// пример входного параметра для метода  единичной проверки.
        /// </summary>
        [OperationContract]
        string GetSingleCheckQuerySample();


        /// <summary>
        /// пример входного параметра для метода пакетной проверки;
        /// </summary>
        [OperationContract]
        string GetBatchCheckQuerySample();


        /// <summary>
        /// выполняет проверку одного свидетельства;
        /// </summary>
        [OperationContract]
        string SingleCheck(string lastname, string firstname, string patronymicname, string passportseria, string passportnumber, string certificatenumber, string typographicnumber);

        /// <summary>
        /// выполняет проверку одного свидетельства (Тест);
        /// </summary>
        [OperationContract]
        string SingleCheckTest(string lastname, string firstname, string patronymicname, string passportseria, string passportnumber, string certificatenumber, string typographicnumber);


        ///// <summary>
        ///// выполняет пакетную проверку свидетельств;
        ///// </summary>
        ///// <param name="queryXML"></param>
        //[OperationContract]
        //string BatchCheck();

        ///// <summary>
        ///// возвращает результаты пакетной проверки свидетельств.
        ///// </summary>
        ///// <param name="queryXML"></param>
        //[OperationContract]
        //string GetBatchCheckResult(); 

        // TODO: Добавьте здесь операции служб
    }
}
