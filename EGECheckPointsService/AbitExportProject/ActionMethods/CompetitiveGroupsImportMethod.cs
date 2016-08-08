using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Controllers;
using AbitExportProject.Data;
using Fdalilib.Actions2016.BatchApplicationImport;

namespace AbitExportProject.ActionMethods
{
    class CompetitiveGroupsImportMethod : BaseProxyMethod<Root, TError, ImportPackageInfo>, IBaseMethod
    {
        protected override string MethodName => "CompetitiveGroupsImportMethod";
        public int Year => DateTime.Today.Year;
        public override
        string ToString()
        {
            return "Экспортировать информацию о конкурсных группах";
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
                    AdmissionInfo = GetCompetitiveGroupsInfo(mainCtx, Year)
                };
                ImportPackageInfo expRes = null;
                try
                {
                    expRes = proxy.ReturnOrNullAndError(Package, "ImportPack");
                }
                catch (Exception exception)
                {
                    GetImportException(exception);
                }
                

                if (expRes == null) return false;
                SavePackNumber(expRes.PackageID);
                return true;
            }
        }

        private PackageDataAdmissionInfo GetCompetitiveGroupsInfo(UGTUDataDataContext mainCtx, int year)
        {
            
            var campGroups = new List<PackageDataAdmissionInfoCompetitiveGroup>();

            //выбираем все кампании года
            foreach (var campain in mainCtx.Abit_Campaigns.Where(x => x.YearFrom == year))
            {
                foreach (var campGroup in mainCtx.Abit_CompetitiveGroups.Where(x => x.Abit_Campaign == campain))
                {
                    campGroups.Add(
                        new PackageDataAdmissionInfoCompetitiveGroup
                        {
                            UID = campGroup.id_group.ToString(),
                            CampaignUID = campain.UID.ToString(),
                            Name = campGroup.Name,
                            EducationLevelID = (uint) campGroup.EducationLevel.IDItem,
                            DirectionID = (uint) campGroup.SpecID,
                            EducationSourceID = (uint) campGroup.EducSource.IDItem,
                            EducationFormID = (uint) campGroup.FormEd.IDItem,
                            IsForKrym = campGroup.IsForKrim.Value,
                            IsAdditional = campGroup.IsAdditional.Value,
                            EntranceTestItems = GetExams(mainCtx, year, campGroup),
                            TargetOrganizations = GetTargetOrganization(mainCtx, year, campGroup)
                        });
                }
            }

            var admissionInfo = new PackageDataAdmissionInfo()
            {
                CompetitiveGroups = campGroups
            };

            return admissionInfo;
        }

        /// <summary>
        /// Экзамены по конкурсной группе
        /// </summary>
        /// <param name="mainCtx"></param>
        /// <param name="year"></param>
        /// <param name="campGroup"></param>
        /// <returns></returns>
        private static List<PackageDataAdmissionInfoCompetitiveGroupEntranceTestItem> GetExams(UGTUDataDataContext mainCtx, int year, Abit_CompetitiveGroup campGroup)
        {
            
            var allNaborsBySpec = mainCtx.ABIT_Diapazon_spec_facs.Where(x => (x.NNyear == year) &&
                                                                             (x.Relation_spec_fac.EducationBranch.ik_FB ==
                                                                              campGroup.SpecID) &&
                                                                             (x.Relation_spec_fac.Form_ed.ik_FB ==
                                                                              campGroup.FormEdID));

            var examToFis = new List<PackageDataAdmissionInfoCompetitiveGroupEntranceTestItem>();

            var includedExams = new List<int>();
            foreach (var spec in allNaborsBySpec)
            {
                foreach (var exam in spec.ABIT_Diapazon_Discs.Select(x => x.ABIT_Disc).ToList().Distinct())
                {
                    if (includedExams.Contains(exam.ik_disc)) continue;
                    includedExams.Add(exam.ik_disc);

                    var defaultExam = spec.ABIT_Diapazon_Discs.FirstOrDefault(x => x.ABIT_Disc == exam);
                    
                    var ex = new PackageDataAdmissionInfoCompetitiveGroupEntranceTestItem
                    {
                        UID = defaultExam.ik_disc_nabor.ToString() + campGroup.EducSourceID,  //у нас они не уникальны в рамках категории оплаты, а ФИС требует уникальные номера
                        EntranceTestSubject = exam.ik_FB == null
                            ? new TEntranceTestSubject {SubjectName = exam.сname_disc.Trim()}
                            : new TEntranceTestSubject {SubjectID = (uint)exam.ik_FB},
                        MinScore = defaultExam.Min_ball ?? 25,
                        EntranceTestTypeID = (uint) exam.ik_FB_type,
                        EntranceTestPriority = 1
                    };
                    examToFis.Add(ex);
                }
            }

            return examToFis;
        }

        private static string GetUniqueUID(ABIT_Diapazon_Disc defaultExam,
            List<PackageDataAdmissionInfoCompetitiveGroupEntranceTestItem> examToFis)
        {
            var uid = defaultExam.ik_disc_nabor;
            while (examToFis.Select(x => x.UID).Contains(uid.ToString())) uid++;
            return uid.ToString();
        }

        private static List<PackageDataAdmissionInfoCompetitiveGroupTargetOrganization> GetTargetOrganization(
            UGTUDataDataContext mainCtx, int year, Abit_CompetitiveGroup campGroup)
        {
            var abitsTarg = mainCtx.ABIT_postups.Where(x => (x.idTarget != null)&&(x.ABIT_Diapazon_spec_fac.NNyear == year)
            &&(x.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_FB == campGroup.SpecID)
            &&(x.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB == campGroup.FormEdID)
            &&(x.Kat_zach.TypeKatZach.ik_FB == campGroup.EducSourceID)
            &&(x.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.Direction.ik_FB == campGroup.EducationLevelID));

            var orgs = abitsTarg.Select(y => y.Abit_Target.TargetOrganization).ToList().Distinct();
            var targets = new List<PackageDataAdmissionInfoCompetitiveGroupTargetOrganization>();
            foreach (var org in orgs)
            {
                var item = NumberTargetType.NumberTargetO;
                switch (campGroup.FormEdID)
                {
                    case 11: item = NumberTargetType.NumberTargetO;
                        break;
                    case 10: item = NumberTargetType.NumberTargetZ;
                        break;
                    case 12: item = NumberTargetType.NumberTargetOZ;
                        break;
                }

                targets.Add(new PackageDataAdmissionInfoCompetitiveGroupTargetOrganization()
                {
                    UID = org.id.ToString(),
                    CompetitiveGroupTargetItem = new PackageDataAdmissionInfoCompetitiveGroupTargetOrganizationCompetitiveGroupTargetItem()
                    {
                        Item = (uint) abitsTarg.Count(),
                        ItemElementName = item
                    },
                });
            }

            return targets;
        }
    }
}
