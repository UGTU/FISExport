using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using AbitExportProject.Data;
using Fdalilib.Actions2016.BatchApplicationImport;

namespace AbitExportProject.ActionMethods
{
    class ApplicationsImportMethod: BaseProxyMethod<Root, TError, ImportPackageInfo>, IBaseMethod
    {
        public int Year => DateTime.Today.Year;
        protected override string MethodName => "ApplicationsImportMethod";   

        public override string ToString()
        {
            return "Импортировать новые и изменившиеся заявления";
        }

        protected override void SetAuth()
        {
            Package.AuthData.Login = Login;
            Package.AuthData.Pass = Password;
        }

        public bool Run(Func<string, string> askMore)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                Package.PackageData = new PackageData()
                {
                    Applications = GetCurrentApps(mainCtx, Year, 0, 0)
                };

                var expRes = proxy.ReturnOrNullAndError(Package, "ImportPack");

                if (expRes == null) return false;
                SavePackNumber(expRes.PackageID);
                return true;
            }
        }

        public static List<PackageDataApplication> GetCurrentApps(UGTUDataDataContext ctx, int year, int specIk, int facIk) //если SpecIK == 0, то импортируем все специальности
        {                                                                                                                   //если FacIK == 0, то импортируем все институты

            var persons = ctx.ABIT_postups.Where(x =>
                                                (x.ABIT_Diapazon_spec_fac.NNyear == year) &&                                        //за текущий год
                                                ((x.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_spec == specIk)     //фильтр специальности                      
                                                 || (specIk == 0)) &&
                                                ((x.ABIT_Diapazon_spec_fac.Relation_spec_fac.ik_fac == facIk)                       //фильтр института
                                                 || (facIk == 0)) &&
                                                (x.ABIT_sost_zach.ik_FB != null)                                                    //в тех состояних, которые предусматриваются в ФИС
                                                && !x.Student.Person.Export_FB_journal.Is_actual).Select(s => s.Student.Person).Distinct();

            var apps = new List<PackageDataApplication>();
            var NotPerson = new List<Person>();
            //var ind = 0;
            var set = new List<decimal>()   { 167778, 167793, 167808, 167901, 167936, 167937, 167951, 168020, 168027, 168057
                                            , 168078, 168138, 168166, 168185, 168194, 168196, 168224, 168247, 168365, 168415
                                            , 168457, 168490, 168507, 168514, 168572, 168613, 168614, 168663, 168798, 168804
                                            , 168871, 168912, 168913, 168923, 168928, 168934, 168935, 168953, 168960, 168962
                                            , 168967, 168979, 168984, 168992, 169019, 169057, 169067, 169098, 169103, 169124
                                            , 169142, 169164, 169223, 169268, 169284, 169317, 169335, 169336, 169345, 169359
                                            , 169364, 169378, 169381, 169404, 169406, 169440, 169444, 169472, 169474, 169475
                                            , 169483, 169500, 169545, 169550, 169572, 169603, 169610, 169613, 169615, 169635
                                            , 169641, 169648, 169652, 169655, 169672, 169695, 169697, 169709, 169723, 169770
                                            , 169800, 169803, 169804, 169812, 169820, 169822, 169849, 169850, 169851, 169857
                                            , 169863, 169869, 169877, 169887, 169911, 169920, 169933, 169934, 169977, 169990
                                            , 170018, 170023, 170038, 170045, 170046, 170049, 170076, 170091, 170110, 170141
                                            , 170157, 170160, 170169, 170178, 170184, 170189, 170193, 170194, 170198, 170206
                                            , 170207, 170210, 170213, 170216, 170220, 170225, 170243, 170250, 170255, 170265
                                            , 170282, 170284, 170285, 170287, 170289, 170299, 170300, 170326, 170334, 170337
                                            , 170345, 170346, 170361, 170363, 170382, 170386, 170391, 170394, 170395, 170434
                                            , 170477, 170480, 170481, 170498, 170510, 170516, 170519, 170558, 170565, 170572
                                            , 170585, 170594, 170607, 170609, 170632, 170645, 170657, 170663, 170665, 170667
                                            , 170682, 170699, 170707, 170713, 170779, 170875, 170884, 170912, 170968, 170990
                                            , 171083, 171154, 171203, 171252, 171260, 171295, 171313, 171369, 171370, 171389
                                            , 171394, 171410, 171416, 171421, 171423, 171424, 171442 };
            foreach (var person in persons)
            {
                //проверка документов, без которой пакет вообще не будет принят сервисом
                if (!person.IsAllDocsCorrect()) continue; 

                if (set.Contains(person.nCode)) 
                {
                    NotPerson.Add(person);                 
                    continue;   
                }
                apps.Add(BuildApplicationPackage(person, ctx, year));
            }
            if (NotPerson.Count > 0) Fdalilib.LogWriter.MakeLog("Не все студенты попали в пакет:");
            foreach (var item in NotPerson)
            {
                Fdalilib.LogWriter.MakeLog(string.Format("{0}: {1} {2} {3}", item.nCode, item.Clastname, item.Cfirstname, item.Cotch));
            }
            return apps;
        }

        /// <summary>
        /// Создаёт экземпляр Application с данными по указанному абитуриенту, используя заданный контекст ИС УГТУ
        /// </summary>
        /// <param name="person">Персона-абитуриент в ИС УГТУ</param>
        /// <param name="ctx">Контекст ИС УГТУ</param>
        /// <param name="year">Год поступления</param>
        /// <returns>Экземпляр Application с данными абитуриента</returns>
        internal static PackageDataApplication BuildApplicationPackage(Person person, UGTUDataDataContext ctx, int year)
        {
            //все его заявления (кроме поданных по сети)
            var applications = ctx.ABIT_postups.Where(x => (x.nCode == person.nCode) && (x.ik_zach != ABIT_postup.NetworkState)).ToList();

            //сначала отбираем заявление с зачисленным, потом - с текущим состоянием
            var application = applications.FirstOrDefault(x => x.IsMain || x.IsZachisl || x.IsCurrent || x.IsZachislAnotherApplication) ?? applications.FirstOrDefault(); 

            var isReal = applications.Any(x => x.Realy_postup) || applications.Any(x => x.IsZachisl);
  
            //идентификационный документ
            var docI = person.IdentityDoc;
            //другие идентификационные документы для подгрузки ЕГЭ
            List<PackageDataApplicationApplicationDocumentsIdentityDocument> otherDocs = null;
            if (person.IdentityDocs.Count > 1)
            {
                otherDocs = PackOtherIdentityDocs(person, docI, isReal, application);
            }

            //образовательный документ
            var docEdu = person.EducationalDoc;

            List<PackageDataApplicationEntranceTestResult> eTestResult = null;

            var finS = new List<PackageDataApplicationFinSourceEduForm>();
            //заполняем конкурсные группы
            foreach (var postup in applications.Where(x => !x.IsNetwork && !x.IsReplaced)) //отсеиваем тех, кто был подан по сети и переведенных
            {
                var specID = postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_FB;
                var compGroup = ctx.Abit_CompetitiveGroups.ToList().FirstOrDefault(x => (x.SpecID == specID)
                                                                                          && (x.FormEdID == postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB) 
                                                                                          && (x.EducSourceID == postup.Kat_zach.TypeKatZach.ik_FB)
                                                                                          && (x.EducationLevelID == postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.Direction.ik_FB));

                //Debug.Assert(compGroup != null, "BuildApplicationPackage: compGroup != null");
                if (compGroup == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не найдена конкурсная группа для заявления {0}", postup.NN_abit));
                //Debug.Assert(postup.Kat_zach.TypeKatZach.ik_FB != null, "postup.Kat_zach.TypeKatZach.ik_FB != null");
                if (postup.Kat_zach.TypeKatZach.ik_FB == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указан код для \"{0}\" по ФИС {0}", postup.Kat_zach.TypeKatZach));
                //Debug.Assert(postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB.HasValue, "postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB != null");
                if (postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указан код для \"{0}\" по ФИС {0}", postup.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.Cname_form_ed));

                finS.Add(new PackageDataApplicationFinSourceEduForm
                {
                    CompetitiveGroupUID = compGroup.id_group.ToString(),
                    IsAgreedDate = postup.RegistrationDate,
                    IsAgreedDateSpecified = isReal,
                    IsForSPOandVO = true,
                    TargetOrganizationUID = (postup.idTarget != null) ? postup.Abit_Target.idTargetOrganization.ToString() : "0",
                });

                //заполняем результаты вступительных испытаний
                foreach (var disc in postup.ABIT_Diapazon_spec_fac.ABIT_Diapazon_Discs)             //для всех дисциплин направления       
                {
                    var exam = postup.ABIT_Vstup_exams.SingleOrDefault(x => x.ik_disc == disc.ik_disc);

                    if (exam?.cosenka == null || exam.ABIT_VidSdachi.IsEGE) continue;

                    //Debug.Assert(exam.cosenka != null, "exam.cosenka != null");
                    if (exam.cosenka == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не проставлена оценка по {0} по {1}", exam.ABIT_VidSdachi.cname_sdach, exam.ABIT_Disc.сname_disc));

                    var ets = new TEntranceTestSubject();
                    if (exam.ABIT_Disc.ik_FB != null) ets.SubjectID = (uint)exam.ABIT_Disc.ik_FB;
                    else ets.SubjectName = exam.ABIT_Disc.сname_disc.Trim();

                    if (eTestResult == null) eTestResult = new List<PackageDataApplicationEntranceTestResult>();
                    else if (eTestResult.Any(x => (x.EntranceTestSubject.Equals(ets)) && (x.CompetitiveGroupUID == compGroup.id_group.ToString()))) continue;

                    var resDoc = new InstitutionDocument()
                    {
                        DocumentNumber = (exam.NNvedom.Trim() != "") ? exam.NNvedom.Trim() : "ф-1",
                        DocumentDate =
                            exam.ABIT_Rassadka?.ABIT_Raspisanie.date_of.ToString("yyyy-MM-dd") ?? "2016-07-16",
                        DocumentTypeID = 1
                    };

                    eTestResult.Add(new PackageDataApplicationEntranceTestResult
                    {
                        UID = exam.id.ToString(),
                        CompetitiveGroupUID = compGroup.id_group.ToString(),
                        EntranceTestSubject = ets,
                        EntranceTestTypeID = (uint)exam.ABIT_Disc.ik_FB_type,
                        ResultValue = (decimal)exam.cosenka,
                        ResultSourceTypeID = (uint)exam.ABIT_VidSdachi.ik_FB,
                        ResultDocument = new PackageDataApplicationEntranceTestResultResultDocument()
                        {
                            Item = resDoc
                        }
                    });
                }
            }
            //Debug.Assert(docI.Dd_vidan != null, "docI.Dd_vidan != null");
            if (docI.Dd_vidan == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указана дата выдачи \"{0}\", у абитуриента [nCode]{1}", docI.document.cvid_doc, person.nCode));
            //Debug.Assert(person.Dd_birth != null, "person.Dd_birth != null");
            if (person.Dd_birth == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указана дата рождения, у абитуриента [nCode]{0}", person.nCode));
            //Debug.Assert(docI.document.ik_FB != null, "docI.document.ik_FB != null");
            if (docI.document.ik_FB == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указан тип документа \"{0}\" по ФИС, у абитуриента [nCode]{1}", docI.document.cvid_doc, person.nCode));

            //Debug.Assert(person.grazd.ik_FB != null, "person.grazd.ik_FB != null " + person.Clastname + " " + person.Cfirstname);   //предусмотреть ситуацию, когда гражданство не указано
            if (person.grazd.ik_FB == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указано гражданство, у абитуриента [nCode]{0}", person.nCode));

            var app = new PackageDataApplication
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

                RegistrationDate = application.RegistrationDate,
                NeedHostel = (person.Lobchegit != null) && person.Lobchegit.GetValueOrDefault(),

                //если уже зачислен, то импортировать с состоянием "Текущее", иначе с состоянием "Непрошедий проверку"
                StatusID = (uint)application.StatusId,
                FinSourceAndEduForms = finS,
                EntranceTestResults = eTestResult,
                ApplicationDocuments = new PackageDataApplicationApplicationDocuments
                {
                    IdentityDocument = PackOneIdentDoc(person, application.OriginalReceivedDate, docI),
                    EduDocuments = PackEduDocuments(year, docEdu, application, isReal),
                    OtherIdentityDocuments = otherDocs,
                },
                IndividualAchievements = GetIndividualAchievments(person)
            };

            var mem = new MemoryStream();
            var ser = new XmlSerializer(app.GetType());
            try
            {
                ser.Serialize(mem, app);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }
            
          
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

        private static List<PackageDataApplicationApplicationDocumentsIdentityDocument> PackOtherIdentityDocs(Person person, Doc_stud docI, bool isReal, ABIT_postup application)
        {
            return person.IdentityDocs.Where(x => x.Ik_doc != docI.Ik_doc).Select(doc => PackOneIdentDoc(person, application.OriginalReceivedDate, doc)).ToList();
        }

        private static PackageDataApplicationApplicationDocumentsIdentityDocument PackOneIdentDoc(Person person, string originalReceivedDate, Doc_stud doc)
        {
            return new PackageDataApplicationApplicationDocumentsIdentityDocument()
            {
                OriginalReceivedDate = originalReceivedDate,
                DocumentNumber = doc.Np_number,
                DocumentSeries = doc.Cd_seria,
                DocumentDate = doc.Date,
                IdentityDocumentTypeID =
                    (uint)
                        (string.IsNullOrEmpty(Convert.ToString(doc.document.ik_subFB))
                            ? Doc_stud.OtherIdentity
                            : doc.document.ik_subFB),
                BirthDate = person.BirthDay,
                NationalityTypeID = (uint) (person.grazd.ik_FB ?? -1) //елси не указано гражданство
            };
        }

        private static List<PackageDataApplicationIndividualAchievement> GetIndividualAchievments(Person person)
        {
            return (from bonuse in person.AllDocs.Select(x => x.Abit_Bonuse)
                where bonuse != null
                select new PackageDataApplicationIndividualAchievement()
                {
                    IAMark = (uint) bonuse.balls, IADocumentUID = bonuse.ik_doc.ToString(), IAUID = bonuse.ik_doc.ToString()
                }).ToList();
        }

        private static List<PackageDataApplicationApplicationDocumentsEduDocument> PackEduDocuments(int year, Doc_stud docEdu, ABIT_postup application, bool isReal)
        {
            if (docEdu?.document.ik_FB == null) return null;
            var eduDocuments = new List<PackageDataApplicationApplicationDocumentsEduDocument>();
            EduDocItemChoiceType eduType;
            object eItem;

            //Debug.Assert(docEdu.Dd_vidan != null, "docEdu.Dd_vidan != null");
            if (docEdu.Dd_vidan == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указана дата выдачи \"{0}\", у абитуриента [nCode]{1}", docEdu.document.cvid_doc, docEdu.nCode));

            switch ((int)docEdu.document.ik_FB)
            {
                case 3: //Аттестат о среднем (полном) общем образовании
                    eduType = EduDocItemChoiceType.SchoolCertificateDocument;
                    eItem = new TSchoolCertificateDocument
                    {
                        DocumentDate = docEdu.Dd_vidan.Value,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceivedDate = application.OriginalReceivedDate
                    };
                    break;
                case 4: //Диплом о высшем профессиональном образовании
                    eduType = EduDocItemChoiceType.HighEduDiplomaDocument;
                    eItem = new THighEduDiplomaDocument()
                    {
                        DocumentDate = docEdu.Dd_vidan.Value,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceivedDate = application.OriginalReceivedDate
                    };
                    break;
                case 5: //"5">Диплом о среднем профессиональном образовании
                    //Debug.Assert(!string.IsNullOrEmpty(docEdu.Cd_seria), "docEdu.Cd_seria in MiddleEduDiplomaDocument. NCode=" + docEdu.nCode);
                    if (string.IsNullOrEmpty(docEdu.Cd_seria)) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указана серия \"{0}\" у абитуриент [nCode]{1}", docEdu.document.cvid_doc, docEdu.nCode));

                    eduType = EduDocItemChoiceType.MiddleEduDiplomaDocument;
                    eItem = new TMiddleEduDiplomaDocument()
                    {
                        DocumentDate = docEdu.Dd_vidan.Value,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceivedDate = application.OriginalReceivedDate
                    };
                    break;
                case 6: //"6">Диплом о начальном профессиональном образовании  
                    eduType = EduDocItemChoiceType.BasicDiplomaDocument;
                    eItem = new TBasicDiplomaDocument()
                    {
                        DocumentDate = docEdu.Dd_vidan.Value,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceivedDate = application.OriginalReceivedDate
                    };
                    break;
                case 16: //Аттестат об основном общем образовании
                    eduType = EduDocItemChoiceType.SchoolCertificateBasicDocument;
                    eItem = new TSchoolCertificateDocument()
                    {
                        DocumentDate = docEdu.Dd_vidan.Value,
                        GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                        DocumentSeries = docEdu.Cd_seria,
                        DocumentNumber = docEdu.Np_number,
                        DocumentOrganization = docEdu.Cd_kem_vidan,
                        OriginalReceivedDate = application.OriginalReceivedDate
                    };
                    break;
                default:
                    {
                        eduType = EduDocItemChoiceType.SchoolCertificateDocument;
                        eItem = new TSchoolCertificateDocument()
                        {
                            DocumentDate = docEdu.Dd_vidan.Value,
                            GPA = (application.SchoolAverMark != null) ? (float)application.SchoolAverMark : 0,
                            DocumentSeries = docEdu.Cd_seria,
                            DocumentNumber = docEdu.Np_number,
                            DocumentOrganization = docEdu.Cd_kem_vidan,
                            OriginalReceivedDate = application.OriginalReceivedDate
                        };
                        break;
                    }
            }
            eduDocuments.Add(new PackageDataApplicationApplicationDocumentsEduDocument { Item = eItem, ItemElementName = eduType });
            return eduDocuments;
        }
    }
}
