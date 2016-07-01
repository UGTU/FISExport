using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib.Actions2016.DictionaryDetails;

namespace AbitExportProject.ParsersToDB
{
    public static partial class DictionaryParser
    {

        public static void ParseDictionaryItems(UGTUDataDataContext mainCtx, DictionaryDataDictionaryItem dictItem,
            int dictionaryCode)
        {
            if (mainCtx.DictionaryContents.Any(
                x => (x.IDItem == dictItem.ID) && (x.DictionaryCode == dictionaryCode))) return;
            var dict_item = new DictionaryContent()
            {
                IDItem = (int)dictItem.ID,
                DictionaryCode = dictionaryCode,
                Name = dictItem.Name
            };
            mainCtx.DictionaryContents.InsertOnSubmit(dict_item);
        }
    }
}
