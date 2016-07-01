using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib;
using Fdalilib.Actions2016.Dictionary;

//using Fdalilib.Actions2016.Dictionary;

namespace AbitExportProject.ParsersToDB
{
    
    public static partial class DictionaryParser
    {

        public static void ParseDictionary(UGTUDataDataContext mainCtx, DictionariesDictionary dictionary)
        {
            if (mainCtx.DictionaryLists.Any(x => x.Code == dictionary.Code)) return;
            var dict = new DictionaryList()
            {
                
                Code = (int)dictionary.Code,
                Name = dictionary.Name
            };
            mainCtx.DictionaryLists.InsertOnSubmit(dict);
        }
    }
}
