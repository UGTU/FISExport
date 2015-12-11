using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace Fdalilib.Service
{
    /// <summary>
    /// Интерфейс определяет типы, реализующие доступ к сервисам интеграции федеральной информационной системы приёма заявлений
    /// </summary>
    [ContractClass(typeof(FisProxyServiceContract))]
    public interface IFisProxyService
    {
        /// <summary>
        /// Получает перечень справочников федеральной системы
        /// </summary>
        /// <param name="authData">Данные аутентификации в составе XML елемента Root.AuthData</param>
        /// <returns>Перечеь справочников в составе XML елемента Dictionaries</returns>
        XElement GetDictionaries(XElement authData);

        /// <summary>
        /// Получает содержимое справочника, заданного иденитфикатором из числа возвращённых методом GetDictionaries
        /// </summary>
        /// <param name="dictionaryContent">Данные аутентификации в составе XML елемента Root.AuthData идентификатора справочника в составе Root.GetDictionaryContent</param>
        /// <returns>Содержимое справочника в составе XML элемента типа DictionaryData</returns>
        XElement GetDictionaryDetails(XElement dictionaryContent);

        /// <summary>
        /// Получает результат проверки одного заявления абитуриента
        /// </summary>
        /// <param name="xElement"></param>
        /// <returns></returns>
        XElement GetSingleCheckApp(XElement xElement);

        /// <summary>
        /// Импорт одного заявления абитуриента
        /// </summary>
        /// <param name="xElement">Данные заявления</param>
        /// <returns>Результат импорта</returns>
        XElement ImportSingle(XElement xElement);

        /// <summary>
        /// Валидация пакета заявлений абитуриента
        /// </summary>
        /// <param name="xElement">Пакет заявлений</param>
        /// <returns>Результат валидации</returns>
        XElement ValidatePack(XElement xElement);

        /// <summary>
        /// Импорт пакета заявлений абитуриента
        /// </summary>
        /// <param name="xElement">Пакет заявления</param>
        /// <returns>Результат импорта</returns>
        XElement ImportPack(XElement xElement);

        /// <summary>
        /// Получение результата импорта пакета заявлений абитуриента
        /// </summary>
        /// <param name="xElement">Пакет заявления</param>
        /// <returns>Результат импорта</returns>
        XElement ImportPackResult(XElement xElement);

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="xElement">Информация для удаления</param>
        /// <returns>Результат удаления с идентификатором</returns>
        XElement DeleteData(XElement xElement);

        /// <summary>
        /// Результат удаления 
        /// </summary>
        /// <param name="xElement">Пакет, попытка удаления которого была произведена</param>
        /// <returns>Результат удаления с конфликтами</returns>
        XElement DeleteDataResult(XElement xElement);
        
        /// <summary>
        ///     Получает результат проверки пакета заявлений
        /// </summary>
        /// <param name="xElement">Параметры запроса: аутентификационные данные, идентификатор пакета заявлений</param>
        /// <returns></returns>
        XElement GetCheckPack(XElement xElement);

        /// <summary>
        ///     Возвращает информацию о образовательном учреждении
        /// </summary>
        /// <param name="xElement">Идентификационная информация, (опционально) идентификатор образовательного учреждения</param>
        /// <returns>Информация о университете</returns>
        XElement GetUniversityInfo(XElement xElement);
    }
}
