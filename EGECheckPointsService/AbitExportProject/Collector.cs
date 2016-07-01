using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using AbitExportProject.Data;
using Fdalilib.Actions2015;

namespace AbitExportProject
{
    /// <summary>
    ///     Производит сбор комплексных элементов для отправки пакетов
    /// </summary>
    public static class Collector
    {

        const int IsNetwork = 9;
        const int IsReplaced = 2;
        const int OtherIdentity = 9;
        const int IsCurrent = 4;

        const int SPO = 5;
        const int Ochn = 1;
        const int Zaoch = 2;
        const int OZaoch = 7;

        const int EGE = 5;


        const int Budjet = 1;
        const int CKP = 2;

        /// <summary>
        /// Производит сбор всех текущих абитуриентов
        /// </summary>
        public static Application[] GetCurrentApps(UGTUDataDataContext ctx, int year, int specIk, int facIk)   //если SpecIK == 0, то импортируем все специальности
        {                                                                                                      //если FacIK == 0, то импортируем все институты

            var persons =
                ctx.ABIT_postups.Where(x =>
                    (x.ABIT_Diapazon_spec_fac.NNyear == year) &&                                        //за текущий год
                    ((x.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_spec == specIk)     //фильтр специальности                      
                     || (specIk == 0)) &&
                    ((x.ABIT_Diapazon_spec_fac.Relation_spec_fac.ik_fac == facIk)                       //фильтр института
                     || (facIk == 0)) &&
                    (x.ABIT_sost_zach.ik_FB != null)                                                    //в тех состояних, которые предусматриваются в ФИС
                    && !x.IsActual).Select(s => s.Student.Person).Distinct();

            var apps = new Application[persons.Count()];
            var ind = 0;
            foreach (var person in persons)
            {
                if (!person.IsAllDocsCorrect()) continue;   //проверка документов, без которой пакет вообще не будет принят сервисом
                apps[ind++] = BuildApplicationPackage(person, ctx, year);
            }
            return apps;
        }

        /// <summary>
        ///     Создаёт экземпляр Application с данными по указанному абитуриенту, используя заданный контекст ИС УГТУ
        /// </summary>
        /// <param name="person">Персона-абитуриент в ИС УГТУ</param>
        /// <param name="ctx">Контекст ИС УГТУ</param>
        /// <param name="year">Год поступления</param>
        /// <returns>Экземпляр Application с данными абитуриента</returns>
        internal static Application BuildApplicationPackage(Person person, UGTUDataDataContext ctx, int year)
        {                                                                                             
            var applications = ctx.ABIT_postups.Where(x => (x.nCode == person.nCode) && (x.ik_zach != IsNetwork));  //все его заявления (кроме поданных по сети)

            var application = 
                applications.FirstOrDefault(x => x.IsMain || x.IsZachisl || x.IsCurrent)??  //сначала отбираем заявление с зачисленным, потом - с текущим состоянием
                applications.FirstOrDefault();   
            var isReal = applications.Any(x=>x.Realy_postup) || applications.Any(x => x.IsZachisl);

            var compGroup = new List<string>();
            var specGroup = new List<string>(); 
            var finS = new List<PackageDataApplicationFinSourceEduForm>(); 

            //идентификационный документ
            var docI = person.IdentityDoc;
            //другие идентификационные документы для подгрузки ЕГЭ


            List<PackageDataApplicationApplicationDocumentsIdentityDocument1> oDocs = null;
            if (person.IdentityDocs.Count > 1)
            {
                oDocs = PackOtherIdentityDocs(person, docI, isReal, application);
            }

            //образовательный документ
            var docEdu = person.EducationalDoc;            

            List<PackageDataApplicationEntranceTestResult> eTestResult = null;
            var egeDocs = new List<PackageDataApplicationApplicationDocumentsEgeDocument>();

            //заполняем конкурсные группы
            foreach (ABIT_postup postup in applications.Where(x => (x.ik_zach != IsNetwork) && (x.ik_zach != IsReplaced))) //отсеиваем тех, кто был подан по сети и переведенных
            {
                var specUID = postup.ABIT_Diapazon_spec_fac.Main_NNRecord_FB.ToString();
                specGroup.Add(specUID); //Как?

                Debug.Assert(postup.Kat_zach.TypeKatZach.ik_FB != null, "postup.Kat_zach.TypeKatZach.ik_FB != null");
                Debug.Assert(postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB.HasValue, "postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB != null");

                finS.Add(new PackageDataApplicationFinSourceEduForm()
                {
                   FinanceSourceID = (uint)postup.Kat_zach.TypeKatZach.ik_FB,
                   EducationFormID = (uint)postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB,
                   CompetitiveGroupID = postup.ABIT_Diapazon_spec_fac.Main_NNRecord_FB.ToString(),
                   CompetitiveGroupItemID = postup.ABIT_Diapazon_spec_fac.Main_NNRecord_FB.ToString(),
                   TargetOrganizationUID = (postup.idTarget != null) ? postup.Abit_Target.idTargetOrganization.ToString() : "0"
                });

                compGroup.Add(specUID);
               
                //заполняем результаты вступительных испытаний
                foreach (var disc in postup.ABIT_Diapazon_spec_fac.ABIT_Diapazon_Discs)                        //для всех дисциплин направления       
                {
                     var exam = postup.ABIT_Vstup_exams.SingleOrDefault(x => x.ik_disc == disc.ik_disc);

                     if ((exam == null) || (!exam.cosenka.HasValue)||(exam.ABIT_VidSdachi.ik_sdach == EGE)) continue;

                     Debug.Assert(exam.cosenka != null, "exam.cosenka != null");
                     var ets = new TEntranceTestSubject();
                     if (exam.ABIT_Disc.ik_FB != null) ets.SubjectID = (uint) exam.ABIT_Disc.ik_FB;
                       else ets.SubjectName = exam.ABIT_Disc.сname_disc.Trim();

                     if (eTestResult == null) eTestResult = new List<PackageDataApplicationEntranceTestResult>();
                     else if (eTestResult.Any(x => (x.EntranceTestSubject.Equals(ets)) && (x.CompetitiveGroupID == specUID))) continue;
             
                     eTestResult.Add(new PackageDataApplicationEntranceTestResult()
                        {
                           UID = exam.id.ToString(),
                           CompetitiveGroupID = specUID,
                           EntranceTestSubject = ets,
                           EntranceTestTypeID = (uint) exam.ABIT_Disc.ik_FB_type,
                           ResultValue = (decimal)exam.cosenka,
                           ResultSourceTypeID = (uint)exam.ABIT_VidSdachi.ik_FB,
                           ResultDocument = new PackageDataApplicationEntranceTestResultResultDocument()
                           {
                               InstitutionDocument = new TInstitutionDocument()
                               {
                                   DocumentNumber = (exam.NNvedom.Trim() != "") ? exam.NNvedom.Trim() : "ф-1",
                                   DocumentDate = ((exam.ABIT_Rassadka != null) ? exam.ABIT_Rassadka.ABIT_Raspisanie.date_of.ToString("yyyy-MM-dd") : "2015-07-15"),
                                   DocumentTypeID = 1
                               }
                           }
                        });
                }
            }

            Debug.Assert(application.ABIT_sost_zach.ik_FB != null, "application.ABIT_sost_zach.ik_FB != null");
            Debug.Assert(docI.Dd_vidan != null, "docI.Dd_vidan != null");
            Debug.Assert(person.Dd_birth != null, "person.Dd_birth != null");
            Debug.Assert(docI.document.ik_FB != null, "docI.document.ik_FB != null");
            Debug.Assert(person.grazd.ik_FB != null, "person.grazd.ik_FB != null " + person.Clastname + " " + person.Cfirstname);             //предусмотреть ситуацию, когда гражданство не указано
            var app = new Application
            {
                UID = person.nCode.ToString(),
                Entrant = new PackageDataApplicationEntrant
                {
                    FirstName = person.Cfirstname,
                    LastName = person.Clastname,
                    MiddleName = person.Cotch,
                    GenderID = (uint)(2 - Convert.ToInt32(person.lSex)),
                    UID = person.nCode.ToString()
                },

                ApplicationNumber = Convert.ToString(application.nCode), 

                RegistrationDate = application.dd_pod_zayav,
                NeedHostel = (person.Lobchegit != null) && (person.Lobchegit.GetValueOrDefault()),
                NeedHostelSpecified = true,
                StatusID = (application.IsZachisl) ? IsCurrent : (uint)application.ABIT_sost_zach.ik_FB, //если уже зачислен, то импортировать с состоянием "Текущее"
                StatusIDSpecified = true,
                SelectedCompetitiveGroups = compGroup,
                SelectedCompetitiveGroupItems = specGroup,
                FinSourceAndEduForms = finS,
                EntranceTestResults = eTestResult,
                ApplicationDocuments = new PackageDataApplicationApplicationDocuments
                {
                    IdentityDocument = new PackageDataApplicationApplicationDocumentsIdentityDocument
                    {
                        OriginalReceived = isReal,
                        OriginalReceivedDate = application.OriginalReceivedDate,
                        DocumentNumber = docI.Number,
                        DocumentSeries = docI.Seria,
                        DocumentDate = docI.Date,
                        IdentityDocumentTypeID = (uint)(string.IsNullOrEmpty(Convert.ToString(docI.document.ik_subFB)) ? OtherIdentity : docI.document.ik_subFB),
                        BirthDate = person.BirthDay,
                        NationalityTypeID = (uint)person.grazd.ik_FB
                    },
                    EduDocuments = CreateEduDocument(year, docEdu, application, isReal),
                    OtherIdentityDocuments = oDocs
                }

            };
            
            var mem = new MemoryStream();
            var ser = new XmlSerializer(app.GetType());
            ser.Serialize(mem, app);
            mem.Position = 0;
            var xmlApp = XElement.Load(mem);

            //---------------------------------------запись в журнал--------------------------------------------------------------------------------
            var expRecord = ctx.Export_FB_journals.SingleOrDefault(x => (x.nCode == person.nCode));
            if (expRecord == null)
            {
                expRecord = new Export_FB_journal
                {
                    nCode = person.nCode,
                    NNYear = year
                };
                ctx.Export_FB_journals.InsertOnSubmit(expRecord);
            }
            expRecord.Is_actual = true;
            expRecord.Import_result = "Is exported";
            expRecord.Registration_Date = application.dd_pod_zayav;
            expRecord.Date_beg_import = DateTime.Now;
            expRecord.xml_str = xmlApp;

            //--------------------------------------------------------------------------------------------------------------------------------------

            return app;
        }

        private static List<PackageDataApplicationApplicationDocumentsIdentityDocument1> PackOtherIdentityDocs(Person person, Doc_stud docI, bool isReal, ABIT_postup application)
        {
            return person.IdentityDocs.Where(x => x.Ik_doc != docI.Ik_doc).Select(doc => new PackageDataApplicationApplicationDocumentsIdentityDocument1()
            {
                OriginalReceived = isReal,
                OriginalReceivedDate = application.OriginalReceivedDate,
                DocumentNumber = doc.Np_number,
                DocumentSeries = doc.Cd_seria,
                DocumentDate = doc.Date,
                IdentityDocumentTypeID = (uint) (string.IsNullOrEmpty(Convert.ToString(doc.document.ik_subFB)) ? OtherIdentity : doc.document.ik_subFB),
                BirthDate = person.BirthDay,
                NationalityTypeID = (uint) person.grazd.ik_FB
            }).ToList();
        }

        private static List<PackageDataApplicationApplicationDocumentsEduDocument> CreateEduDocument(int year, Doc_stud docEdu, ABIT_postup application, bool isReal)
        {
            if ((docEdu == null) || (docEdu.document.ik_FB == null)) return null;
            var eduDocuments = new List<PackageDataApplicationApplicationDocumentsEduDocument>();
            EduDocItemChoiceType eduType;
            object eItem;

            //
                   
            switch ((int) docEdu.document.ik_FB)
            {
                case 3: //Аттестат о среднем (полном) общем образовании
                    eduType = EduDocItemChoiceType.SchoolCertificateDocument;
                    eItem = new TSchoolCertificateDocument()
                    {
                        DocumentDate =
                            (docEdu.Dd_vidan == null) ? Convert.ToDateTime("2015-06-20") : (DateTime) docEdu.Dd_vidan,
                        GPA = (application.SchoolAverMark != null) ? (float) application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceived = isReal,
                        OriginalReceivedDate = application.OriginalReceivedDate,
                    };
                    break;
                case 4: //Диплом о высшем профессиональном образовании
                    eduType = EduDocItemChoiceType.HighEduDiplomaDocument;
                    eItem = new THighEduDiplomaDocument()
                    {
                        DocumentDate =
                            (docEdu.Dd_vidan == null) ? Convert.ToDateTime("2015-06-20") : (DateTime)docEdu.Dd_vidan,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceived = isReal,
                        OriginalReceivedDate = application.dd_pod_zayav.ToString("yyyy-MM-dd"),
                    };
                    break;
                case 5: //"5">Диплом о среднем профессиональном образовании
                    Debug.Assert(!string.IsNullOrEmpty(docEdu.Cd_seria), "docEdu.Cd_seria in MiddleEduDiplomaDocument. NCode=" + docEdu.nCode);
                    eduType = EduDocItemChoiceType.MiddleEduDiplomaDocument;
                    eItem = new TMiddleEduDiplomaDocument()
                    {
                        DocumentDate =
                            (docEdu.Dd_vidan == null) ? Convert.ToDateTime("2015-06-20") : (DateTime)docEdu.Dd_vidan,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceived = isReal,
                        OriginalReceivedDate = application.dd_pod_zayav.ToString("yyyy-MM-dd"),
                    };
                    break;
                case 6: //"6">Диплом о начальном профессиональном образовании  
                    eduType = EduDocItemChoiceType.BasicDiplomaDocument;
                    eItem = new TBasicDiplomaDocument()
                    {
                        DocumentDate =
                            (docEdu.Dd_vidan == null) ? Convert.ToDateTime("2015-06-20") : (DateTime)docEdu.Dd_vidan,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceived = isReal,
                        OriginalReceivedDate = application.dd_pod_zayav.ToString("yyyy-MM-dd"),
                    };
                    break;
                case 16: //Аттестат об основном общем образовании
                    eduType =EduDocItemChoiceType.SchoolCertificateBasicDocument;
                    eItem = new TSchoolCertificateDocument()
                    {
                        DocumentDate =
                            (docEdu.Dd_vidan == null) ? Convert.ToDateTime("2015-06-20") : (DateTime)docEdu.Dd_vidan,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceived = isReal,
                        OriginalReceivedDate = application.dd_pod_zayav.ToString("yyyy-MM-dd"),
                    };
                    break;
                default:
                {
                    eduType = EduDocItemChoiceType.SchoolCertificateDocument;
                    eItem = new TSchoolCertificateDocument()
                    {
                        DocumentDate =
                            (docEdu.Dd_vidan == null) ? Convert.ToDateTime("2015-06-20") : (DateTime)docEdu.Dd_vidan,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceived = isReal,
                        OriginalReceivedDate = application.dd_pod_zayav.ToString("yyyy-MM-dd"),
                    };
                    break;
                }   
            }
            eduDocuments.Add(new PackageDataApplicationApplicationDocumentsEduDocument(){Item = eItem, ItemElementName = eduType});
            return eduDocuments;
        }

        /// <summary>
        /// Возвращает всех абитуриентов, которые по каким-то причинам не были импортированы
        /// </summary>
        /// <returns></returns>
        //private static string[] GetNonExportApplication(int year, UGTUDataDataContext mainCtx)
        //{
        //    var sDao = AbitServiceDao.Instance;
        //  //  var mainCtx = CreateDatabaseContext();

        //    var impApps = sDao.GetExportedApps();                                   //все экспортированные заявления
        //    var allApps = mainCtx.Export_FB_journals.Where(x => x.NNYear == year);  //все наши заявления

        //    return allApps.Select(x => x.nCode.ToString()).Except(impApps.Select(y => y.UID)).ToArray();
        //}
        
        

        

        
    }
}
