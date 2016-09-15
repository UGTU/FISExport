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
    class AdmissionVolumeImportMethod : BaseProxyMethod<Root, TError, ImportPackageInfo>, IBaseMethod
    {
        protected override string MethodName => "AdmissionVolumeImportMethod";
        public int Year => DateTime.Today.Year;
        public override string ToString()
        {
            return "Экспортировать информацию об объеме и структуре приема";
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
                    AdmissionInfo = GetAdmissionVolumeInfo(mainCtx, Year)
                };

                var expRes = proxy.ReturnOrNullAndError(Package, "ImportPack");

                if (expRes == null) return false;
                SavePackNumber(expRes.PackageID);
                return true;                
            }
        }

        private PackageDataAdmissionInfo GetAdmissionVolumeInfo(UGTUDataDataContext mainCtx, int year)
        {
            //выбираем все кампании года
            var campaigns = mainCtx.Abit_Campaigns.Where(x => x.YearFrom == year).ToList();

            var admVolume = new List<PackageDataAdmissionInfoItem>();
            //заполняем данные по каждой кампании
            foreach (var campain in campaigns)
            {
                //все направления по поступлению, соответствующие типу кампании
                var nabors = mainCtx.ABIT_Diapazon_spec_facs.Where(x => x.NNyear == year)
                    .Where(x => x.Relation_spec_fac.EducationBranch.Direction.TypeDirection.ik_FIS_TypePK == campain.ik_FIS_TypePK);

                //IDs из ФИС для указанной кампании
                var abitSpecs = nabors.Select(x => x.Relation_spec_fac.EducationBranch).Select(x=>x.ik_FB).ToList().Distinct();

                //выгружаем все контр цифры приема по специальностям для кампании
                foreach (var educBranch in abitSpecs)
                {
                    var allNaborsBySpec = nabors.Where(x => x.Relation_spec_fac.EducationBranch.ik_FB == educBranch);
                    //за образец берется специальность с наименьшим именем
                    var ourSpec = mainCtx.EducationBranches.Where(x => x.ik_FB == educBranch).OrderBy(x=>x.Cname_spec.Length).FirstOrDefault();        
                    
                    //Debug.Assert(ourSpec != null, "OurSpec != null");
                    if (ourSpec == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не найдена специальность для {0}", nabors.Select(x => x.Relation_spec_fac.EducationBranch).First(x => x.ik_FB == educBranch).Cname_qualif));

                    var levelIk = ourSpec.Direction.ik_FB;
                    
                    //Debug.Assert(levelIk != null, "levelIk != null");
                    if (levelIk == null) Fdalilib.LogWriter.MakeLog(string.Format("Ошибка. Не указан код для {0} по ФИС", ourSpec.Direction.cName_direction));

                    var AdmissionInfo = new PackageDataAdmissionInfoItem
                    {
                        UID = ourSpec.ik_spec.ToString(), //abitSpec.NNrecord.ToString(),
                        CampaignUID = campain.UID.ToString(),
                        EducationLevelID = (uint)ourSpec.Direction.ik_FB,
                        DirectionID = (uint)educBranch,
                        //бюджет
                        NumberBudgetO = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestBudjet) ?? 0),
                        NumberBudgetZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestBudjet) ?? 0),
                        NumberBudgetOZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestBudjet) ?? 0),
                        //договор (устанавливаем плановые цифры только в том случае, если набор по данной форме обучения есть)
                        NumberPaidO = (uint)(allNaborsBySpec.Count(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)) > 0 ? 100 : 0),
                        NumberPaidZ = (uint)(allNaborsBySpec.Count(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)) > 0 ? 100 : 0),
                        NumberPaidOZ = (uint)(allNaborsBySpec.Count(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)) > 0 ? 100 : 0),
                        //ЦКП
                        NumberTargetO = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        NumberTargetZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        NumberTargetOZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        //особое право
                        NumberQuotaO = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        NumberQuotaZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0),
                        NumberQuotaOZ = (uint)(allNaborsBySpec.Where(y => (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.IdOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIk)).Sum(z => z.MestCKP) ?? 0)
                    };
                    //отправляем только там, где есть цифры приема
                    AdmissionInfo.NumberBudgetOSpecified = AdmissionInfo.NumberBudgetO > 0;
                    AdmissionInfo.NumberBudgetOZSpecified = AdmissionInfo.NumberBudgetOZ > 0;
                    AdmissionInfo.NumberBudgetZSpecified = AdmissionInfo.NumberBudgetZ > 0;
                    AdmissionInfo.NumberPaidOSpecified = AdmissionInfo.NumberPaidO > 0;
                    AdmissionInfo.NumberPaidOZSpecified = AdmissionInfo.NumberPaidOZ > 0;
                    AdmissionInfo.NumberPaidZSpecified = AdmissionInfo.NumberPaidZ > 0;
                    AdmissionInfo.NumberQuotaOSpecified = AdmissionInfo.NumberQuotaO > 0;
                    AdmissionInfo.NumberQuotaOZSpecified = AdmissionInfo.NumberQuotaOZ > 0;
                    AdmissionInfo.NumberQuotaZSpecified = AdmissionInfo.NumberQuotaZ > 0;

                    admVolume.Add(AdmissionInfo);


                    /*abitSpec.Main_NNRecord_FB = abitSpec.NNrecord;
                    var exams = abitSpec.ABIT_Diapazon_Discs.Select(disc => new PackageDataAdmissionInfoCompetitiveGroupEntranceTestItem()
                    {
                        UID = disc.ik_disc_nabor.ToString(),
                        EntranceTestTypeID = (uint)(disc.ABIT_Disc.ik_FB_type),
                        //Form = (disc.ABIT_Disc.ik_FB != null ? "ЕГЭ" : "Вступительное испытание ОУ"),
                        EntranceTestSubject = /*(disc.ABIT_Disc.ik_FB != null) ?
                    new TEntranceTestSubject()
                    {
                     SubjectID = (uint)(disc.ABIT_Disc.ik_FB)
                    } :
                        new TEntranceTestSubject()
                        {
                            //SubjectName = disc.ABIT_Disc.сname_disc.Trim()
                        }
                    }).ToList();*/

                }

            }
            var admissionInfo = new PackageDataAdmissionInfo()
            {
                AdmissionVolume = admVolume
            };

            return admissionInfo;


            /* foreach (var abitSpec in abitSpecs.Where(abitSpec => (abitSpec.Relation_spec_fac.EducationBranch.Direction.ik_FB.HasValue) &&
                                                                  (abitSpec.Relation_spec_fac.EducationBranch.ik_FB.HasValue)
                                                                  ))
             {
                 //  var specIk = abitSpec.Relation_spec_fac.ik_spec;
                 var directIK = abitSpec.Relation_spec_fac.EducationBranch.ik_FB;
                 var levelIK = abitSpec.Relation_spec_fac.EducationBranch.Direction.ik_FB;
                 if (admVolume.Exists(x => (x.DirectionID == abitSpec.Relation_spec_fac.EducationBranch.ik_FB)
                                           && (x.EducationLevelID == levelIK)))
                 {
                     //abitSpec.Main_NNRecord_FB = Convert.ToInt32(
                     //    admVolume.Single(x => (x.DirectionID == abitSpec.Relation_spec_fac.EducationBranch.ik_FB)
                     //                                 && (x.EducationLevelID == levelIK))
                     //        .NNRecord);
                     continue;
                 }

                 var cGi = new PackageDataAdmissionInfoItem
                 {
                     //тут уместнее код специальности?
                     UID = abitSpec.Relation_spec_fac.ik_spec.ToString(), //abitSpec.NNrecord.ToString(),
                     CampaignUID = campain.UID.ToString(),
                     EducationLevelID = (uint)abitSpec.Relation_spec_fac.EducationBranch.Direction.ik_FB,
                     DirectionID = (uint)abitSpec.Relation_spec_fac.EducationBranch.ik_FB,
                     //бюджет
                     NumberBudgetO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
                     NumberBudgetZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
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


                 abitSpec.Main_NNRecord_FB = abitSpec.NNrecord;
                 var exams = abitSpec.ABIT_Diapazon_Discs.Select(disc => new PackageDataAdmissionInfoCompetitiveGroupEntranceTestItem()
                 {
                     UID = disc.ik_disc_nabor.ToString(),
                     EntranceTestTypeID = (uint)(disc.ABIT_Disc.ik_FB_type),
                     //Form = (disc.ABIT_Disc.ik_FB != null ? "ЕГЭ" : "Вступительное испытание ОУ"),
                     EntranceTestSubject = /*(disc.ABIT_Disc.ik_FB != null) ?
                 new TEntranceTestSubject()
                 {
                  SubjectID = (uint)(disc.ABIT_Disc.ik_FB)
                 } :
                     new TEntranceTestSubject()
                     {
                     //SubjectName = disc.ABIT_Disc.сname_disc.Trim()
                 }
                 }).ToList();
*/

            ////конкурсные группы
            //compGroups.Add(new PackageDataAdmissionInfoCompetitiveGroup()
            //{
            //    CampaignUID = currCampain.UID.ToString(),
            //    UID = abitSpec.NNrecord.ToString(),
            //    Course = 1,
            //    Name =
            //        abitSpec.Relation_spec_fac.EducationBranch.Cname_spec + "(" +
            //        abitSpec.Relation_spec_fac.EducationBranch.Direction.cShort_name_direction + ")",
            //    Items = new List<PackageDataAdmissionInfoCompetitiveGroupCompetitiveGroupItem>() { cGi }
            //});

            //if (exams.Count > 0) compGroups[compGroups.Count - 1].EntranceTestItems = exams;

            //объем и структура приема
            //var aV = new PackageDataAdmissionInfoItem
            //{
            //    UID = abitSpec.NNrecord.ToString(),
            //    CampaignUID = currCampain.UID.ToString(),
            //    Course = 1,
            //    EducationLevelID = (uint)abitSpec.Relation_spec_fac.EducationBranch.Direction.ik_FB,
            //    DirectionID = (uint)abitSpec.Relation_spec_fac.EducationBranch.ik_FB,
            //    NNRecord = abitSpec.NNrecord,  //доп. поле
            //    NumberBudgetO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
            //    NumberBudgetZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
            //    NumberBudgetOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0)
            //};

            //бюджет
            //договор (устанавливаем плановые цифры только в том случае, если набор по данной форме обучения есть)
            //if (abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0)
            //    aV.NumberPaidO = 50;
            //if (abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0)
            //    aV.NumberPaidZ = 50;
            //if (abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0)
            //    aV.NumberPaidOZ = 50;
            ////особое право
            //aV.NumberQuotaO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestLgot) ?? 0);
            //aV.NumberQuotaZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestLgot) ?? 0);
            //aV.NumberQuotaOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestLgot) ?? 0);
            ////целевой прием
            //aV.NumberTargetO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Ochn) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0);
            //aV.NumberTargetZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == Zaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0);
            //aV.NumberTargetOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == OZaoch) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0);
            ////отправляем только там, где есть цифры приема
            //aV.NumberBudgetOSpecified = aV.NumberBudgetO > 0;
            //aV.NumberBudgetOZSpecified = aV.NumberBudgetOZ > 0;
            //aV.NumberBudgetZSpecified = aV.NumberBudgetZ > 0;
            //aV.NumberPaidOSpecified = aV.NumberPaidO > 0;
            //aV.NumberPaidOZSpecified = aV.NumberPaidOZ > 0;
            //aV.NumberPaidZSpecified = aV.NumberPaidZ > 0;
            //aV.NumberQuotaOSpecified = aV.NumberQuotaO > 0;
            //aV.NumberQuotaOZSpecified = aV.NumberQuotaOZ > 0;
            //aV.NumberQuotaZSpecified = aV.NumberQuotaZ > 0;
            //aV.NumberTargetOSpecified = aV.NumberTargetO > 0;
            //aV.NumberTargetOZSpecified = aV.NumberTargetOZ > 0;
            //aV.NumberTargetZSpecified = aV.NumberTargetZ > 0;

            //admVolume.Add(aV);



        }
    }
}
