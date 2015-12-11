using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using AbitExportProject.Data;
using Fdalilib.ImportClasses;

namespace AbitExportProject
{
    /// <summary>
    ///     Производит сбор комплексных элементов для отправки пакетов
    /// </summary>
    public static class Collector
    {

        const int IsNerwork = 9;
        const int IsReplaced = 2;
        const int OtherIdentity = 9;
        const int IsCurrent = 4;
        const int IsZachisl = 2;
        const int SPO = 5;
        const int Ochn = 1;
        const int Zaoch = 2;
        const int OZaoch = 7;
        const int CurrentState = 4;
        const int EGE = 5;
        const int MiddleEduDiplomaDocument = 7;
        const int HighEduDiplomaDocument = 9;
        const int VremDocument = 12;
        const int Budjet = 1;
        const int CKP = 2;

        /// <summary>
        ///     Производит сбор всех текущих абитуриентов
        /// </summary>
        public static Application[] GetCurrentApps(UGTUDataDataContext ctx, int year, int specIk, int facIk)   //если SpecIK == 0, то импортируем все специальности
        {                                                                                                      //если FacIK == 0, то импортируем все институты
            var newAppCodes = new List<int>();

            foreach (var aPost in ctx.ABIT_postups.Where(x =>
                (x.ABIT_Diapazon_spec_fac.NNyear == year) && //за текущий год
                ((x.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_spec == specIk) //фильтр специальности
                 || (specIk == 0)) &&
                ((x.ABIT_Diapazon_spec_fac.Relation_spec_fac.ik_fac == facIk) //фильтр института
                 || (facIk == 0)) &&
                (x.ABIT_sost_zach.ik_FB != null) //в тех состояних, которые предусматриваются в ФИС
                && ((x.Student.Person.Export_FB_journal.Is_actual == null)||(x.Student.Person.Export_FB_journal.Is_actual ==false)) //новые или изменившиеся
                //&& (x.nCode == 113310)
                ))

            {
                var res_exp = ctx.Export_FB_journals.FirstOrDefault(y => y.nCode == aPost.nCode);
                
                //цикл проверок------------------------------------------------------------------------------------------------------------
                if (aPost.Student.Person.Doc_studs.Count(y => y.document.IsIdentity) == 0)
                {SetError(res_exp, "Нет идентификационного документа"); continue;}
                
                if ((aPost.Student.Person.Doc_studs.Any(y => y.document.IsEducational))
                    &&(aPost.Student.Person.Doc_studs.FirstOrDefault(y => y.document.IsEducational).Ik_vid_doc == MiddleEduDiplomaDocument)
                    && (aPost.Student.Person.Doc_studs.FirstOrDefault(y => y.document.IsEducational).Cd_seria.Trim() == "")) 
                { SetError(res_exp, "Диплом о проф. образовании должен иметь серию");continue;}

                if ((aPost.Student.Person.Doc_studs.Any(y => y.document.IsEducational))
                    &&(aPost.Student.Person.Doc_studs.FirstOrDefault(y => y.document.IsEducational).Ik_vid_doc == HighEduDiplomaDocument)
                && (aPost.Student.Person.Doc_studs.FirstOrDefault(y => y.document.IsEducational).Cd_seria.Trim() == "")) 
                { SetError(res_exp, "Диплом о высшем образовании должен иметь серию");continue;}

                if(aPost.Student.Person.Doc_studs.FirstOrDefault(y => y.document.IsIdentity).Dd_vidan == null)
                {SetError(res_exp, "в идентификационном документе должна быть дата выдачи");continue;}

                if (aPost.Student.Person.Doc_studs.Any(y => y.Dd_vidan > DateTime.Today))
                { SetError(res_exp, "Даты документов должны быть выданы в прошлом, а не в будущем"); continue; }
                //-------------------------------------------------------------------------------------------------------------------------

                if (!newAppCodes.Contains((int) aPost.nCode)) //если такого студента еще не было
                    newAppCodes.Add((int) aPost.nCode);
            }

            var apps = new Application[newAppCodes.Count()];
            var ind = 0;


            foreach (var abitPostup in newAppCodes)
            {
                apps[ind++] = BuildApplicationPackage(abitPostup, ctx, year);
            }
            ctx.SubmitChanges();  //сохранить записи в журнале
            return apps;
        }

        private static void SetError(Export_FB_journal res_exp, string message)
        {
            res_exp.Import_result = message;
            res_exp.Is_actual = null;
        }

        /// <summary>
        ///     Создаёт экземпляр Application с данными по указанному абитуриенту, используя заданный контекст ИС УГТУ
        /// </summary>
        /// <param name="abitid">Идентификатор абитуриента в ИС УГТУ</param>
        /// <param name="ctx">Контекст ИС УГТУ</param>
        /// <param name="year">Год поступления</param>
        /// <returns>Экземпляр Application с данными абитуриента</returns>
        internal static Application BuildApplicationPackage(decimal abitid, UGTUDataDataContext ctx, int year)
        {
            var student = ctx.Students.Single(x => x.nCode == abitid);                                                  //студент   
            var person = ctx.Persons.Single(x => x.nCode == abitid);                                                    //как персона   
            var applications = ctx.ABIT_postups.Where(x => (x.Student == student) && (x.ik_zach != IsNerwork));         //все его заявления (кроме поданных по сети)

            var application = 
                applications.FirstOrDefault(x => ((x.ABIT_sost_zach.ik_type_zach == IsZachisl) || (x.ABIT_sost_zach.ik_FB == CurrentState)))??  //сначала отбираем заявление с текущим состоянием
                applications.FirstOrDefault();   
            var isReal = ((applications.Count(x => x.Realy_postup) > 0) || (applications.Any(x => x.ABIT_sost_zach.ik_type_zach == IsZachisl)));

            var compGroup = new List<string>();
            var specGroup = new List<string>(); 
            var finS = new List<PackageDataApplicationFinSourceEduForm>(); 

            //идентификационный документ
            var docI = person.Doc_studs.Single(x => (x.document.IsIdentity) && (x.Dd_vidan == person.Doc_studs.Where(y=>y.document.IsIdentity).Max(y => y.Dd_vidan)));
            //другие идентификационные документы для подгрузки ЕГЭ


            List<PackageDataApplicationApplicationDocumentsIdentityDocument1> oDocs = null;
            if (person.Doc_studs.Where(x => (x.document.IsIdentity)&&(x.Dd_vidan != null)).Count()>1)
            {
               oDocs = new List<PackageDataApplicationApplicationDocumentsIdentityDocument1>();
               foreach (var Doc in person.Doc_studs.Where(x => (x.document.IsIdentity) && (x.Ik_doc != docI.Ik_doc)))
                {
                    oDocs.Add(new PackageDataApplicationApplicationDocumentsIdentityDocument1()
                    {
                        OriginalReceived = isReal,
                        OriginalReceivedDate = application.dd_pod_zayav.ToString("yyyy-MM-dd"),
                        DocumentNumber = Doc.Np_number,
                        DocumentSeries = Doc.Cd_seria,
                        DocumentDate = Doc.Dd_vidan.Value.ToString("yyyy-MM-dd"),
                        IdentityDocumentTypeID = (uint)(string.IsNullOrEmpty(Convert.ToString(Doc.document.ik_subFB)) ? OtherIdentity : Doc.document.ik_subFB),
                        BirthDate = person.Dd_birth.Value.ToString("yyyy-MM-dd"),
                        NationalityTypeID = (uint)person.grazd.ik_FB
                    });
                }
        
            }

            //образовательный документ
            var docEdu = person.Doc_studs.FirstOrDefault(x => x.document.IsEducational);            

            List<PackageDataApplicationEntranceTestResult> eTestResult = null;
            var egeDocs = new List<PackageDataApplicationApplicationDocumentsEgeDocument>();

            //заполняем конкурсные группы
            foreach (ABIT_postup postup in applications.Where(x => (x.ik_zach != IsNerwork) && (x.ik_zach != IsReplaced))) //отсеиваем тех, кто был подан по сети и переведенных
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

                  /*  if ((exam.ABIT_VidSdachi.ik_sdach == EGE) && (exam.NNvedom.Trim() != ""))
                    {
                        var nVed = exam.NNvedom.Trim();
                        if (!egeDocs.Any(x => x.DocumentNumber == nVed))
                          egeDocs.Add(new PackageDataApplicationApplicationDocumentsEgeDocument()
                          {
                              UID = exam.id.ToString(),
                              DocumentNumber = nVed,
                              Subjects = new List<PackageDataApplicationApplicationDocumentsEgeDocumentSubjectData>()
                          });

                        var egeDoc = egeDocs.FirstOrDefault(x => x.DocumentNumber == nVed);
                        if (!egeDoc.Subjects.Any(x=>x.SubjectID == (uint) exam.ABIT_Disc.ik_FB))
                            egeDoc.Subjects.Add(new PackageDataApplicationApplicationDocumentsEgeDocumentSubjectData()
                            {
                              SubjectID = (uint) exam.ABIT_Disc.ik_FB,
                              Value = (uint) exam.cosenka
                            });            
                        continue;
                    }*/

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
                UID = Convert.ToString(abitid),
                Entrant = new PackageDataApplicationEntrant
                {
                    FirstName = person.Cfirstname,
                    LastName = person.Clastname,
                    MiddleName = person.Cotch,
                    GenderID = (uint)(2 - Convert.ToInt32(person.lSex)),
                    UID = Convert.ToString(abitid)
                },

                ApplicationNumber = Convert.ToString(application.nCode), 

                RegistrationDate = application.dd_pod_zayav,
                NeedHostel = (person.Lobchegit != null) && (person.Lobchegit.GetValueOrDefault()),
                NeedHostelSpecified = true,
                StatusID = (application.ABIT_sost_zach.ik_type_zach == IsZachisl) ? IsCurrent : (uint)application.ABIT_sost_zach.ik_FB, //если уже зачислен, то импортировать с состоянием "Текущее"
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
                        OriginalReceivedDate = application.dd_pod_zayav.ToString("yyyy-MM-dd"),
                        DocumentNumber = string.IsNullOrEmpty(docI.Np_number.Trim()) ? "010101" : docI.Np_number,
                        DocumentSeries = ((docI.Ik_vid_doc == VremDocument)&&(docI.Cd_seria.Trim().Replace(" ", string.Empty) == ""))?  //пустой серии для Временного удостоверения не должно быть
                          "0000":docI.Cd_seria.Trim().Replace(" ", string.Empty),
                        DocumentDate = docI.Dd_vidan.Value.ToString("yyyy-MM-dd"),
                        IdentityDocumentTypeID = (uint)(string.IsNullOrEmpty(Convert.ToString(docI.document.ik_subFB)) ? OtherIdentity : docI.document.ik_subFB),
                        BirthDate = person.Dd_birth.Value.ToString("yyyy-MM-dd"),
                        NationalityTypeID = (uint)person.grazd.ik_FB
                    },
                    EduDocuments = CreateEduDocument(year, docEdu, application, isReal),
                    OtherIdentityDocuments = oDocs ?? null
                   // EgeDocuments = (egeDocs.Count()>0)?egeDocs:null
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
                        OriginalReceivedDate = application.dd_pod_zayav.ToString("yyyy-MM-dd"),
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
        private static string[] GetNonExportApplication(int year, UGTUDataDataContext mainCtx)
        {
            var sDao = AbitServiceDao.Instance;
          //  var mainCtx = CreateDatabaseContext();

            var impApps = sDao.GetExportedApps();                                   //все экспортированные заявления
            var allApps = mainCtx.Export_FB_journals.Where(x => x.NNYear == year);  //все наши заявления

            return allApps.Select(x => x.nCode.ToString()).Except(impApps.Select(y => y.UID)).ToArray();
        }
        
        public static PackageDataOrderOfAdmission[] GetOrdersOfAdmission(UGTUDataDataContext ctx, int year)
        {
            var orders = new List<PackageDataOrderOfAdmission>();
            foreach (var stud in ctx.Export_FB_journals.Where(x => (x.NNYear == year) ))
            {
                if (!ctx.ABIT_postups.Any(
                    x => (x.nCode == stud.nCode) && (x.ABIT_sost_zach.ik_type_zach == IsZachisl))) continue;

                var jourRecord =
                        ctx.Export_FB_journals.FirstOrDefault(x => x.nCode == stud.nCode);

                //Если были ошибки на этапе импорта заявления 
                if (stud.Import_result != "Is exported")
                {
                    jourRecord.Prikaz_result = "Нельзя передать в приказ из-за ошибок на этапе импорта";
                    jourRecord.Is_actual = false;
                    continue;
                }

                foreach (   //получить заявления, по которому зачислили студента
                    var app in
                        ctx.ABIT_postups.Where(x => (x.nCode == stud.nCode) && (x.ABIT_sost_zach.ik_type_zach == IsZachisl))
                    )
                {

                    var stageVal = ctx.Abit_Campaign_Contents.Single(x => (x.ik_prikaz_zach == app.ik_prikaz_zach) 
                        && (x.id_direction == app.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_direction)
                        && (x.id_form == app.ABIT_Diapazon_spec_fac.Relation_spec_fac.Ik_form_ed)
                        && (x.id_eduSource == app.Kat_zach.ik_type_kat)).stage;

                    if (app.DirectionId != 0)
                    {

                        orders.Add(new PackageDataOrderOfAdmission()
                        {
                            Application = new PackageDataOrderOfAdmissionApplication()
                            {
                                ApplicationNumber = stud.nCode.ToString(),
                                RegistrationDate = (DateTime) ctx.Export_FB_journals.Single(y => y.nCode == stud.nCode).Registration_Date,
                                OrderIdLevelBudget = ((app.Kat_zach.ik_type_kat == Budjet) || (app.Kat_zach.ik_type_kat == CKP)) ?
                                (uint) app.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.FinancingSource.ik_FB : 0
                            },
                            EducationLevelID =
                                (uint) app.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.Direction.ik_FB,
                            DirectionID = (uint) app.DirectionId,
                            EducationFormID = (uint) app.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB,
                            CompetitiveGroupUID = app.ABIT_Diapazon_spec_fac.Main_NNRecord_FB.ToString(),

                            FinanceSourceID = (uint) app.Kat_zach.TypeKatZach.ik_FB,
                            OrderOfAdmissionUID = app.ik_prikaz_zach.ToString()

                        });
                        if (stageVal != null) orders[orders.Count - 1].Stage = (uint) stageVal;
                    }
                }
                
                jourRecord.Prikaz_result = "In Prikaz";
                jourRecord.Date_beg_import = DateTime.Now;
                jourRecord.Is_actual = true;
            }

            return (orders.Any()) ? orders.ToArray() : null;
        }

        public static PackageDataCampaignInfo GetCampaignInfo(UGTUDataDataContext mainCtx, int year, TextWriter logger)
        {
            try
            { 
            var forms = new List<uint>();
            var levels = new List<PackageDataCampaignInfoCampaignEducationLevel>();
            var currCampain = mainCtx.Abit_Campaigns.Single(x => x.year == year);
            logger.WriteLine("Импорт по кампании: {0}",currCampain.name);
            logger.Flush();

            //собираем даты приемной кампании
            var campContent = mainCtx.Abit_Campaign_Contents.Where(x => x.Abit_Campaign.year == year);
            var campdDates = campContent.Select(camp => new PackageDataCampaignInfoCampaignCampaignDate()
            {
                UID = camp.ID_Content.ToString(),
                Course = 1,
                DateStart =  camp.Date_start.Value.Date.ToString(),
                DateEnd = camp.Date_end.Value.Date.ToString(),
                DateOrder = camp.Date_order.Value.Date.ToString(),
                EducationFormID = (uint) camp.Form_ed.ik_FB,
                EducationLevelID = (uint) camp.Direction.ik_FB,
                EducationSourceID = (uint) camp.TypeKatZach.ik_FB,
            }).ToList();
            logger.WriteLine("Собрали даты в campdDates");
            logger.Flush();

            foreach (var abContent in campContent.Where(x=>x.stage.HasValue))
            {
                if (abContent.stage.HasValue)
                  campdDates.Single(y => y.UID == abContent.ID_Content.ToString()).Stage_zach = (uint)abContent.stage.Value;
            }
            logger.WriteLine("Поколдовали с этапами зачисления");
            logger.Flush();

            foreach (var camp in campContent)
            {
                //собираем формы обучения
                if ((!forms.Exists(x => x == camp.Form_ed.ik_FB)) && (camp.Form_ed.ik_FB != null))
                    forms.Add((uint)camp.Form_ed.ik_FB);
                //собираем уровни подготовки
                if ((!levels.Exists(y => y.EducationLevelID == camp.Direction.ik_FB)) && (camp.Direction.ik_FB != null))
                    levels.Add(new PackageDataCampaignInfoCampaignEducationLevel()
                    { 
                        Course = 1,
                        EducationLevelID = (uint)camp.Direction.ik_FB
                    });
            }
            logger.WriteLine("Собрали формы обучения и уровни подготовки");
            logger.Flush();

            var campInfo = new PackageDataCampaignInfo();
            var campItems = new List<PackageDataCampaignInfoCampaign>
            {
                new PackageDataCampaignInfoCampaign()
                {
                    UID = currCampain.id,
                    Name = currCampain.name,
                    StatusID = 1, //1 - Идет набор, 2 - Завершена
                    YearStart = (uint) year,
                    YearEnd = (uint) year,
                    CampaignDates = campdDates,
                    EducationForms = forms,
                    EducationLevels = levels
                }
            };
            logger.WriteLine("Собрали даты в campInfo");
            logger.Flush();

            campInfo.Campaigns = campItems;
            return campInfo;
            }
            catch (Exception exception)
            {
                logger.Write(exception.Message);
                throw;
            } 

        }

        public static PackageDataAdmissionInfo GetAmissionInfo(UGTUDataDataContext mainCtx, int year)
        {
           var currCampain = mainCtx.Abit_Campaigns.Single(x => x.year == year); //выбираем текущую кампанию
           var admVolume = new List<PackageDataAdmissionInfoItem>();
           var compGroups = new List<PackageDataAdmissionInfoCompetitiveGroup>();
           
            //выгрузить все направления по поступлению
           var abitSpecs = mainCtx.ABIT_Diapazon_spec_facs.Where(x => x.NNyear == year);

            foreach (var abitSpec in abitSpecs.Where(abitSpec => (abitSpec.Relation_spec_fac.EducationBranch.Direction.ik_FB.HasValue) &&
                                                                 (abitSpec.Relation_spec_fac.EducationBranch.ik_FB.HasValue)
                                                                 ))
            {
              //  var specIk = abitSpec.Relation_spec_fac.ik_spec;
                var directIK = abitSpec.Relation_spec_fac.EducationBranch.ik_FB;
                var levelIK = abitSpec.Relation_spec_fac.EducationBranch.Direction.ik_FB;
                if (admVolume.Exists(x => (x.DirectionID == abitSpec.Relation_spec_fac.EducationBranch.ik_FB)
                                          && (x.EducationLevelID == levelIK)))
                {
                    abitSpec.Main_NNRecord_FB = Convert.ToInt32(
                        admVolume.Single(x => (x.DirectionID == abitSpec.Relation_spec_fac.EducationBranch.ik_FB)
                                                     && (x.EducationLevelID == levelIK))
                            .NNRecord);
                    continue;
                }


                abitSpec.Main_NNRecord_FB = abitSpec.NNrecord;
                var exams = abitSpec.ABIT_Diapazon_Discs.Select(disc => new PackageDataAdmissionInfoCompetitiveGroupEntranceTestItem()
                {
                    UID = disc.ik_disc_nabor.ToString(), 
                    EntranceTestTypeID = (uint) (disc.ABIT_Disc.ik_FB_type), 
                    Form = (disc.ABIT_Disc.ik_FB != null ? "ЕГЭ" : "Вступительное испытание ОУ"), 
                    EntranceTestSubject = /*(disc.ABIT_Disc.ik_FB != null) ?
                    new TEntranceTestSubject()
                    {
                     SubjectID = (uint)(disc.ABIT_Disc.ik_FB)
                    } :*/
                    new TEntranceTestSubject()
                    {
                        SubjectName = disc.ABIT_Disc.сname_disc.Trim()
                    }
                }).ToList();

                var cGi = new PackageDataAdmissionInfoCompetitiveGroupCompetitiveGroupItem
                {
                    UID = abitSpec.NNrecord.ToString(),
                    
                    EducationLevelID = (uint) abitSpec.Relation_spec_fac.EducationBranch.Direction.ik_FB,
                    DirectionID = (uint) abitSpec.Relation_spec_fac.EducationBranch.ik_FB,
                    //бюджет
                    NumberBudgetO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
                    NumberBudgetZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch)&& (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
                    NumberBudgetOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
                    //договор (устанавливаем плановые цифры только в том случае, если набор по данной форме обучения есть)
                    NumberPaidO = (uint)((abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0) ? 50 : 0),
                    NumberPaidZ = (uint)((abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0) ? 50 : 0),
                    NumberPaidOZ = (uint)((abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0) ? 50 : 0),
                    //особое право
                    NumberQuotaO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestLgot) ?? 0),
                    NumberQuotaZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestLgot) ?? 0),
                    NumberQuotaOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestLgot) ?? 0)
                };
                //отправляем только там, где есть цифры приема
                cGi.NumberBudgetOSpecified = cGi.NumberBudgetO > 0;
                cGi.NumberBudgetOZSpecified = cGi.NumberBudgetOZ > 0;
                cGi.NumberBudgetZSpecified = cGi.NumberBudgetZ > 0;
                cGi.NumberPaidOSpecified = cGi.NumberPaidO > 0;
                cGi.NumberPaidOZSpecified = cGi.NumberPaidOZ > 0;
                cGi.NumberPaidZSpecified = cGi.NumberPaidZ > 0;
                cGi.NumberQuotaOSpecified = cGi.NumberQuotaO > 0;
                cGi.NumberQuotaOZSpecified = cGi.NumberQuotaOZ > 0;
                cGi.NumberQuotaZSpecified = cGi.NumberQuotaZ > 0;

                //конкурсные группы
                compGroups.Add(new PackageDataAdmissionInfoCompetitiveGroup()
                {
                    CampaignUID = currCampain.id,
                    UID = abitSpec.NNrecord.ToString(),
                    Course = 1,
                    Name =
                        abitSpec.Relation_spec_fac.EducationBranch.Cname_spec + "(" +
                        abitSpec.Relation_spec_fac.EducationBranch.Direction.cShort_name_direction + ")",
                    Items = new List<PackageDataAdmissionInfoCompetitiveGroupCompetitiveGroupItem>() {cGi}
                 });

                if (exams.Count > 0) compGroups[compGroups.Count - 1].EntranceTestItems = exams;

                //объем и структура приема
                var aV = new PackageDataAdmissionInfoItem
                {
                    UID = abitSpec.NNrecord.ToString(),
                    CampaignUID = currCampain.id,
                    Course = 1,
                    EducationLevelID = (uint) abitSpec.Relation_spec_fac.EducationBranch.Direction.ik_FB,
                    DirectionID = (uint) abitSpec.Relation_spec_fac.EducationBranch.ik_FB,
                    NNRecord = abitSpec.NNrecord,  //доп. поле
                    NumberBudgetO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
                    NumberBudgetZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
                    NumberBudgetOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0)
                };

                //бюджет
                //договор (устанавливаем плановые цифры только в том случае, если набор по данной форме обучения есть)
                if (abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0)
                  aV.NumberPaidO = 50;
                if (abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0)
                  aV.NumberPaidZ = 50;
                if (abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0)
                  aV.NumberPaidOZ = 50;
                //особое право
                aV.NumberQuotaO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestLgot) ?? 0);
                aV.NumberQuotaZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestLgot) ?? 0);
                aV.NumberQuotaOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestLgot) ?? 0);
                //целевой прием
                aV.NumberTargetO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0);
                aV.NumberTargetZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0);
                aV.NumberTargetOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0);
                //отправляем только там, где есть цифры приема
                aV.NumberBudgetOSpecified = aV.NumberBudgetO > 0;
                aV.NumberBudgetOZSpecified = aV.NumberBudgetOZ > 0;
                aV.NumberBudgetZSpecified = aV.NumberBudgetZ > 0;
                aV.NumberPaidOSpecified = aV.NumberPaidO > 0;
                aV.NumberPaidOZSpecified = aV.NumberPaidOZ > 0;
                aV.NumberPaidZSpecified = aV.NumberPaidZ > 0;
                aV.NumberQuotaOSpecified = aV.NumberQuotaO > 0;
                aV.NumberQuotaOZSpecified = aV.NumberQuotaOZ > 0;
                aV.NumberQuotaZSpecified = aV.NumberQuotaZ > 0;
                aV.NumberTargetOSpecified = aV.NumberTargetO > 0;
                aV.NumberTargetOZSpecified = aV.NumberTargetOZ > 0;
                aV.NumberTargetZSpecified = aV.NumberTargetZ > 0;

                admVolume.Add(aV);
            }

           var admInfo = new PackageDataAdmissionInfo()
           {
               AdmissionVolume = admVolume,
               CompetitiveGroups = compGroups
           };
            mainCtx.SubmitChanges();
            return admInfo;
        }
    }
}
