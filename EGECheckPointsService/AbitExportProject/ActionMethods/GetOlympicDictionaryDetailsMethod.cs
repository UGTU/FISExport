﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Controllers;
using AbitExportProject.Data;
using AbitExportProject.ParsersToDB;
using Fdalilib.Actions2016.DictionaryOlympicDetails;


namespace AbitExportProject.ActionMethods
{
    class GetOlympicDictionaryDetailsMethod : BaseProxyMethod<Root, TError, DictionaryData>, IBaseMethod
    {
        protected override string MethodName => "GetOlympicDictionaryDetailsMethod";

        public override string ToString()
        {
            return "Импорт данных справочника олимпиад из ФИС в БД УГТУ";
        }

        public int Year => DateTime.Today.Year;

        public bool Run(Func<string, string> askMore)
        {
            Package.GetDictionaryContent.DictionaryCode = MagicNumberController.OlympicDictionary;
            var curDict = proxy.ReturnOrNullAndError(Package, "GetDictionaryDetails");

            if (curDict.DictionaryItems == null) return false;
            using (var mainCtx = new UGTUDataDataContext())
            {
                foreach (var dictItem in curDict.DictionaryItems)
                {
                    DictionaryParser.ParseOlympicDictionaryItems(mainCtx, dictItem, MagicNumberController.OlympicDictionary);
                }
                CommitToDb(mainCtx);
            }
            return true;
        }

        protected override void SetAuth()
        {
            Package.AuthData.Login = Login;
            Package.AuthData.Pass = Password;
        }
    }
}
