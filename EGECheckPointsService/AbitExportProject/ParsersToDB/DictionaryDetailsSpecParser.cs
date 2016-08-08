using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib.Actions2016.DictionarySpecDetails;


namespace AbitExportProject.ParsersToDB
{
    public static partial class DictionaryParser
    {
        public static void ParseSpecDictionaryItems(UGTUDataDataContext mainCtx, DictionaryDataDictionaryItem dictItem,
            int dictionaryCode)
        {
            if (mainCtx.SpecDetailDictionaries.Any(x => (uint)x.ID == dictItem.ID)) return;
            var dict_item = new SpecDetailDictionary()
            {
                ID = (int)dictItem.ID,
                Name = dictItem.Name,
                NewCode = dictItem.NewCode,
                QualificationCode = dictItem.QualificationCode,
                UGSCode = dictItem.UGSCode,
                UGSName = dictItem.UGSName
            };
            mainCtx.SpecDetailDictionaries.InsertOnSubmit(dict_item);
        }
    }
}
