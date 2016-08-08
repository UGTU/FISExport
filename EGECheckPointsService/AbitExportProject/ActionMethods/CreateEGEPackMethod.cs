using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;

namespace AbitExportProject.ActionMethods
{
    class CreateEGEPackMethod : IBaseMethod
    {
        private string _question = "Фамилия абитуриента (без указания фамилии пакет сформируется для всех абитуриентов с признаком 'Проверять ЕГЭ'):";
        public string FileName { get; set; } = "EGEPackage";
        public int Year => DateTime.Today.Year;

        public override string ToString()
        {
            return "Сформировать XML-пакет для проверки ЕГЭ абитуриента...";
        }

        public bool Run(Func<string, string> askMore)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                var lastName = (askMore != null) ? askMore(_question) : "";
                using (var file = new StreamWriter(FileName + lastName + DateTime.Today.ToString("yyyy MMMM dd") + @".csv", true, Encoding.UTF8))
                {
                    foreach (
                        var abit in
                            mainCtx.Export_FB_journals.Where(x => (x.NNYear == Year) && (((lastName == "") 
                            && x.Person.Student.ABIT_postups.Any(y=>y.NeedCheckEGE == true)) || (x.Person.Clastname == lastName))
                            ).OrderBy(y => y.Person.Clastname))
                    {
                        PackAbitToFile(mainCtx, abit, file);
                    }
                }
            }
            return true;
        }

        private static void PackAbitToFile(UGTUDataDataContext mainCtx, Export_FB_journal abit, StreamWriter file)
        {
            var stud = mainCtx.Persons.FirstOrDefault(y => y.nCode == abit.nCode);
            foreach (var doc in stud.Doc_studs.Where(y => y.document.IsIdentity))
            {
                file.WriteLine(stud.Clastname.Trim().ToUpper() + "%" + stud.Cfirstname.Trim().ToUpper() + "%" +
                               stud.Cotch.Trim().ToUpper() + "%" + doc.Seria + "%" + doc.Number);               
            }
        }
    }
}
