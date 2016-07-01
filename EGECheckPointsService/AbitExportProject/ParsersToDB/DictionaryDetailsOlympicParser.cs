using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib.Actions2016.DictionaryOlympicDetails;


namespace AbitExportProject.ParsersToDB
{
    public static partial class DictionaryParser
    {
        public static void ParseOlympicDictionaryItems(UGTUDataDataContext mainCtx, DictionaryDataDictionaryItem dictItem,
            int dictionaryCode)
        {
            if (mainCtx.DictionaryContents.Any(
                x => (x.IDItem == dictItem.OlympicID) && (x.DictionaryCode == dictionaryCode))) return;
            var dict_item = new DictionaryContent()
            {
                IDItem = (int) dictItem.OlympicID,
                DictionaryCode = dictionaryCode,
                Name = dictItem.OlympicName
            };
            mainCtx.DictionaryContents.InsertOnSubmit(dict_item);
        }
    }
}
