using System.Collections.Generic;
using System.Linq;
using AbitExportProject.DataDecoders;

namespace AbitExportProject.Data
{

    partial class Person
    {
        const int EGE = 5;
        const int EGE_OUT = 8;

        //идентификационный документ по-умолчанию
        public Doc_stud IdentityDoc => IdentityDocs.OrderByDescending(o => o.Dd_vidan).FirstOrDefault();
        //образовательный документ по-умолчанию
        public Doc_stud EducationalDoc => EducationalDocs.OrderByDescending(o => o.Dd_vidan).FirstOrDefault();
        public string BirthDay => DateTimeDecoder.DateToString(Dd_birth);
        public List<Doc_stud> AllDocs => Student.Person.Doc_studs.ToList();

        public List<Doc_stud> IdentityDocs
        {
            get { return Doc_studs.Where(y => y.document.IsIdentity).ToList(); }
        }

        public List<Doc_stud> EducationalDocs
        {
            get { return Doc_studs.Where(y => y.document.IsEducational).ToList(); }
        }

        

        public List<EgeDocument> EgeDocs
        { 
            get
            {
                var listEge = new List<EgeDocument>();
                var mContext = new UGTUDataDataContext();
                foreach (var app in mContext.ABIT_postups.Where(x => x.nCode == nCode))
                {
                    foreach (var eD in app.ABIT_Vstup_exams.Where(y => (y.ik_sdach == EGE) || (y.ik_sdach == EGE_OUT))) //для всех ЕГЭ-документов
                        if (eD.cosenka != null)
                        {
                            // nSert = eD.NNvedom.Substring(eD.NNvedom.IndexOf('-') - 2, 15);
                            var indStr = eD.NNvedom.IndexOf('-');
                            if (indStr <= 0) continue;
                            var nSert = eD.NNvedom.Substring(eD.NNvedom.IndexOf('-') - 2, 15);
                            if (listEge.All(z => z.DocumentNumber != nSert))
                                //если еще не было сертификата с данным номером
                            {
                                listEge.Add(new EgeDocument() //добавляем сертификат
                                {
                                    DocumentNumber = nSert, //eD.NNvedom,
                                    DocumentYear = "20" + nSert.Substring(nSert.Length-2, 2),
                                    Subjects = new List<Subject>()
                                });
                            }
                            if (
                                listEge.Single(s => s.DocumentNumber == nSert)
                                    .Subjects.All(s => s.SubjectId != eD.ABIT_Disc.ik_FB))
                            {
                                listEge.Single(s => s.DocumentNumber == nSert)
                                    .Subjects.Add(new Subject() //добавляем предмет с оценкой
                                    {
                                        SubjectId = (int) eD.ABIT_Disc.ik_FB,
                                        Value = (int) eD.cosenka
                                    });
                            }
                        }
                }

                return listEge.Count > 0 ? listEge : null;
            }              }
        public void SetError(string message)
        {
            Export_FB_journal.Import_result = message;
            Export_FB_journal.Is_actual = false;
        }
        public bool IsIdentityDocsCorrect()
        {
            if (IdentityDocs.Count == 0)
            {
                SetError("Нет идентификационного документа");
                return false;
            }
            if (IdentityDocs.Any(doc => doc.Dd_vidan == null))
            {
                SetError("В идентификационных документах должна быть дата выдачи");
                return false;
            }
            if (IdentityDocs.Any(doc => (doc.Ik_vid_doc == Doc_stud.VremDocument) && doc.IsEmptySeria))
            {
                SetError("Во временном удостоверении должна быть серия");
                return false;
            }

            return true;
        }

        public bool IsEducationalDocsCorrect()
        {
            if (EducationalDocs.Count == 0)
            {
                SetError("Нет образовательного документа");
                return false;
            }
            if (
                EducationalDocs.Any(
                    doc =>
                        ((doc.Ik_vid_doc == Doc_stud.MiddleEduDiplomaDocument) ||
                         (doc.Ik_vid_doc == Doc_stud.HighEduDiplomaDocument)) && doc.IsEmptySeria))
            {
                SetError("Диплом об образовании должен иметь серию");
                return false;
            }
            return true;
        }

        public bool IsAllDocsCorrect()
        {
            if (AllDocs.Any(a => !a.IsCorrectData))
            {
                SetError("Даты документов должны быть выданы в прошлом, а не в будущем");
                return false;
            }
            if (AllDocs.Any(a => string.IsNullOrEmpty(a.Number)))
            {
                SetError("У всех документов должен быть номер");
                return false;
            }
            return IsIdentityDocsCorrect() && IsEducationalDocsCorrect();
        }

    }
}
        