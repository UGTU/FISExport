using System;
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
                            if (listEge.All(z => z.DocumentNumber != nSert))    //если еще не было сертификата с данным номером
                            {
                                listEge.Add(new EgeDocument()   //добавляем сертификат
                                {
                                    DocumentNumber = nSert,     //eD.NNvedom,
                                    DocumentYear = "20" + nSert.Substring(nSert.Length-2, 2),
                                    Subjects = new List<Subject>()
                                });
                            }
                            if ( listEge.Single(s => s.DocumentNumber == nSert).Subjects.All(s => s.SubjectId != eD.ABIT_Disc.ik_FB))
                            {
                                listEge.Single(s => s.DocumentNumber == nSert).Subjects.Add(new Subject()   //добавляем предмет с оценкой
                                                                                            {
                                                                                                SubjectId = (int) eD.ABIT_Disc.ik_FB,
                                                                                                Value = (int) eD.cosenka
                                                                                            });
                            }
                        }
                }

                return listEge.Count > 0 ? listEge : null;
            }
        }

        public void SetError(string message)
        {
            Export_FB_journal.Import_result = message;
            Export_FB_journal.Is_actual = false;
            Fdalilib.LogWriter.MakeLog(string.Format("Ошибка: Абитуриент с nCode: {0}. \"{1}\".", nCode, message));
            Fdalilib.LogWriter.MakeLog("");
        }

        /// <summary>
        /// Проверка на корректность идентифицирующих документов
        /// </summary>
        /// <returns></returns>
        public bool IsIdentityDocumentsCorrect()
        {
            if (IdentityDocs.Count == 0)
            {
                SetError("Нет идентификационного документа");
                return false;
            }

            var NotCorrect = IdentityDocs.Where(doc => doc.Dd_vidan == null);
            if (NotCorrect?.Count() > 0)
            {
                SetError(string.Format("В идентификационном документе ({0}) должна быть дата выдачи", string.Join(", ", NotCorrect.Select(x => x.document.cvid_doc))));
                return false;
            }

            NotCorrect = IdentityDocs.Where(doc => string.IsNullOrEmpty(doc.Number));
            if (NotCorrect?.Count() > 0)
            {
                SetError(string.Format("В идентификационном документе ({0}) должен быть номер", string.Join(", ", NotCorrect.Select(x => x.document.cvid_doc))));
                return false;
            }


            if (IdentityDocs.Any(x => x.Ik_vid_doc == (int)IdentityDocuments.Passport && string.IsNullOrEmpty(x.Seria)))
            {
                SetError("Паспорт РФ должен иметь серию");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка на корректность документов об образовании
        /// </summary>
        /// <returns></returns>
        public bool IsEducationalDocumentsCorrect()
        {
            if (EducationalDocs.Count == 0)
            {
                SetError("Нет образовательного документа");
                return false;
            }

            var NotCorrect = EducationalDocs.Where(x => (x.Ik_vid_doc == (int)EducationalDocuments.BasicDiploma
                                                           || x.Ik_vid_doc == (int)EducationalDocuments.MiddleEduDiplome
                                                           || x.Ik_vid_doc == (int)EducationalDocuments.IncomplHightEduDiploma
                                                           || x.Ik_vid_doc == (int)EducationalDocuments.HightEduDiploma
                                                           || x.Ik_vid_doc == (int)EducationalDocuments.PostGraduateDiploma
                                                           || x.Ik_vid_doc == (int)EducationalDocuments.PhDDiploma
                                                           || x.Ik_vid_doc == (int)EducationalDocuments.AcademicDiploma)
                                                    && string.IsNullOrEmpty(x.Seria));
            if (NotCorrect?.Count() > 0)
            {
                SetError(string.Format("Документ об образовании ({0}) должен иметь серию", string.Join(", ", NotCorrect.Select(x => x.document.cvid_doc))));
                return false;
            }

            NotCorrect = EducationalDocs.Where(x => x.Ik_vid_doc != (int)EducationalDocuments.Another && string.IsNullOrEmpty(x.Number));
            if (NotCorrect?.Count() > 0)
            {
                SetError(string.Format("Документ об образовании ({0}) должен иметь номер", string.Join(", ", NotCorrect.Select(x => x.document.cvid_doc))));
                return false;
            }

            NotCorrect = EducationalDocs.Where(x => (x.Ik_vid_doc == (int)EducationalDocuments.Another) && (x.Dd_vidan == null || string.IsNullOrEmpty(x.document.cvid_doc)));
            if(NotCorrect?.Count() > 0)
            {
                SetError(string.Format("Иной документ об образовании ({0}) должен иметь и дату выдачи и наименование", string.Join(", ", NotCorrect.Select(x => x.document.cvid_doc))));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка данных на корректность
        /// </summary>
        /// <returns></returns>
        public bool IsAllDataCorrect()
        {
            var NotCorrect = AllDocs.Where(x => x?.Dd_vidan > DateTime.Today);
            //if (NotCorrect?.Count() > 0)
            //{
            //    SetError(string.Format("Дата документа ({0}) должны быть выданы в прошлом, а не в будущем", string.Join(", ", NotCorrect.Select(x => x.document.cvid_doc))));
            //    return false;
            //}

            NotCorrect = AllDocs.Where(a => a.document.ik_FB == null);

            if (NotCorrect?.Count() > 0)
            {
                SetError(string.Format("Для документа ({0}) должны быть проставлены код из справочников ФИС", string.Join(", ", NotCorrect.Select(x => x.document.cvid_doc))));
                return false;
            }

            if (Dd_birth == null)
            {
                SetError("Дата рождения должна быть проставлена");
                return false;
            }

            if (IdentityDoc == null)
            {
                SetError("Идентификационный документ не может отсутствовать");
                return false;
            }

            if (grazd.ik_FB == null)
            {
                SetError("Гражданство не может отсутствовать");
                return false;
            }

            return IsIdentityDocumentsCorrect() && IsEducationalDocumentsCorrect();
        }
    }
}
        