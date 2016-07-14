using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib.Actions2016.BatchApplicationImport;
using AbitExportProject.Controllers;

namespace AbitExportProject.ActionMethods
{
    class BatchApplicationImportMethod: BaseProxyMethod<Root, TError, ImportPackageInfo>, IBaseMethod
    {
        private readonly int OZMagicNumberControlleraoch;

        public int Year => DateTime.Today.Year;
        protected override string MethodName => "BatchApplicationImportMethod";
        /// <summary>
        ///     Производит экспорт в ФИС приказов о зачислении
        /// </summary>
        public void ExportOrdersBatch(int year)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                var pack = new PackageData
                {
                    Orders = new PackageDataOrders()
                    {
                        OrdersOfAdmission = GetOrdersOfAdmission(mainCtx, year)
                    }
                };
                RepeatExportTillResult(pack);
            }
        }

        /// <summary>
        /// экспорт информации о приемной кампании (похоже, импортируется только тогда, когда нет активных данных по этапам и датам)
        /// </summary>
        /// <param name="year">Год</param>
        public void ExportCampaignInfo(int year)
        {

        }

        

        /// <summary>
        /// Экспортировать сведения об объеме и структуре приема
        /// </summary>
        /// <param name="year">Год</param>
        public void ExportAdmissionInfo(int year)
        {
            using (var mainCtx = new UGTUDataDataContext())
            {
                var pack = new PackageData
                {
                    AdmissionInfo = GetAmissionInfo(mainCtx, year),
                };

                RepeatExportTillResult(pack);
            }
        }

        private PackageDataAdmissionInfo GetAmissionInfo(UGTUDataDataContext mainCtx, int year)
        {
            //var currCampain = mainCtx.Abit_Campaigns.Single(x => x.YearFrom == year); //выбираем текущую кампанию
            //выбираем все кампании года
            var Campaigns = mainCtx.Abit_Campaigns.Where(x => x.YearFrom == year).ToList();
            var CampaignsInfo = ExportCampaignInfoMethod.GetCampaignInfo(mainCtx, year);
            //заполняем данные по каждой кампании
            foreach (var compain in Campaigns)
            {
                var admVolume = new List<PackageDataAdmissionInfoItem>();
                var compGroups = new List<PackageDataAdmissionInfoCompetitiveGroup>();

                //все направления по поступлению, соответствующие типу кампании
                var abitSpecs = mainCtx.ABIT_Diapazon_spec_facs.Where(x => x.NNyear == year).Where(x => x.Relation_spec_fac.EducationBranch.Direction.TypeDirection.ik_FIS_TypePK == compain.ik_FIS_TypePK).ToList();

                //выгружаем все контр цифры приема по специальностям для кампании
                foreach (var educBranch in abitSpecs.Select(x => x.Relation_spec_fac.EducationBranch).Distinct().ToList())
                {
                    //  var specIk = abitSpec.Relation_spec_fac.ik_spec;
                    var directIK = educBranch.ik_FB;
                    var levelIK = educBranch.Direction.ik_FB;
                    if (admVolume.Exists(x => (x.DirectionID == directIK)
                                              && (x.EducationLevelID == levelIK)))
                    {
                        //abitSpec.Main_NNRecord_FB = Convert.ToInt32(
                        //    admVolume.Single(x => (x.DirectionID == abitSpec.Relation_spec_fac.EducationBranch.ik_FB)
                        //                                 && (x.EducationLevelID == levelIK))
                        //        .NNRecord);
                        continue;
                    }

                    var AdmissionInfo = new PackageDataAdmissionInfoItem
                    {
                        UID = educBranch.ik_spec.ToString(), //abitSpec.NNrecord.ToString(),
                        CampaignUID = compain.UID.ToString(),
                        EducationLevelID = (uint)educBranch.Direction.ik_FB,
                        DirectionID = (uint)educBranch.ik_FB,
                        //бюджет
                        NumberBudgetO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
                        NumberBudgetZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
                        NumberBudgetOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestBudjet) ?? 0),
                        //договор (устанавливаем плановые цифры только в том случае, если набор по данной форме обучения есть)
                        NumberPaidO = (uint)((abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0) ? 50 : 0),
                        NumberPaidZ = (uint)((abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0) ? 50 : 0),
                        NumberPaidOZ = (uint)((abitSpecs.Count(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)) > 0) ? 50 : 0),
                        //ЦКП
                        NumberTargetO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0),
                        NumberTargetZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0),
                        NumberTargetOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0),
                        //особое право
                        NumberQuotaO = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idOchnFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0),
                        NumberQuotaZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0),
                        NumberQuotaOZ = (uint)(abitSpecs.Where(y => (y.Relation_spec_fac.EducationBranch.ik_FB == directIK) && (y.Relation_spec_fac.Ik_form_ed == MagicNumberController.idOZaochFormEd) && (y.Relation_spec_fac.EducationBranch.Direction.ik_FB == levelIK)).Sum(z => z.MestCKP) ?? 0)
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

                //выгружаем все контр цифры приема по уровням бюджета для кампании
                /*данных таких у нас нет по местному бюджету. Можно использовать ист-к фин-я в EducationBranch для остальных уровней*/
            }


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
                     CampaignUID = compain.UID.ToString(),
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

            var admInfo = new PackageDataAdmissionInfo()
            {
                AdmissionVolume = admVolume,
                CompetitiveGroups = compGroups
            };
            mainCtx.SubmitChanges();
            return admInfo;
        }

        public static List<PackageDataOrdersOrderOfAdmission> GetOrdersOfAdmission(UGTUDataDataContext ctx, int year)
        {
            var orders = new List<PackageDataOrdersOrderOfAdmission>();
            foreach (var stud in ctx.Export_FB_journals.Where(x => (x.NNYear == year)))
            {
                if (!ctx.ABIT_postups.Any(
                    x => (x.nCode == stud.nCode) && x.IsZachisl)) continue;

                /*  var jourRecord =
                           ctx.Export_FB_journals.FirstOrDefault(x => x.nCode == stud.nCode);*/

                //Если были ошибки на этапе импорта заявления 
                if (stud.Import_result != "Is exported")
                {
                    stud.Prikaz_result = "Нельзя передать в приказ из-за ошибок на этапе импорта";
                    stud.Is_actual = false;
                    continue;
                }

                foreach (   //получить заявления, по которому зачислили студента
                    var app in
                        ctx.ABIT_postups.Where(x => (x.nCode == stud.nCode) && (x.IsZachisl))
                    )
                {

                    var stageVal = ctx.Abit_Campaign_Contents.Single(x => (x.ik_prikaz_zach == app.ik_prikaz_zach)
                        && (x.id_direction == app.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_direction)
                        && (x.id_form == app.ABIT_Diapazon_spec_fac.Relation_spec_fac.Ik_form_ed)
                        && (x.id_eduSource == app.Kat_zach.ik_type_kat)).stage;

                    if (app.DirectionId != 0)
                    {

                        orders.Add(new PackageDataOrdersOrderOfAdmission()
                        {
                            //Application = new PackageDataOrdersOrderOfAdmission()
                            //{
                            //    ApplicationNumber = stud.nCode.ToString(),
                            //    RegistrationDate = (DateTime)ctx.Export_FB_journals.Single(y => y.nCode == stud.nCode).Registration_Date,
                            //    OrderIdLevelBudget = ((app.Kat_zach.ik_type_kat == Budjet) || (app.Kat_zach.ik_type_kat == CKP)) ?
                            //    (uint)app.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.FinancingSource.ik_FB : 0
                            //},
                            //EducationLevelID =
                            //    (uint)app.ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.Direction.ik_FB,
                            //DirectionID = (uint)app.DirectionId,
                            //EducationFormID = (uint)app.ABIT_Diapazon_spec_fac.Relation_spec_fac.Form_ed.ik_FB,
                            //CompetitiveGroupUID = app.ABIT_Diapazon_spec_fac.Main_NNRecord_FB.ToString(),

                            FinanceSourceID = (uint)app.Kat_zach.TypeKatZach.ik_FB,
                            OrderOfAdmissionUID = app.ik_prikaz_zach.ToString()

                        });
                        if (stageVal != null) orders[orders.Count - 1].Stage = (uint)stageVal;
                    }
                }

                stud.Prikaz_result = "In Prikaz";
                stud.Date_beg_import = DateTime.Now;
                stud.Is_actual = true;
            }

            return (orders.Any()) ? orders : null;
        }

        protected void RepeatExportTillResult(PackageData pack)
        {
            MakeLog("Всё собрали и пошли отправлять:");
            Console.WriteLine("Всё собрали и пошли отправлять:");

            //var expRes = _sDao.ExportBatch(pack); //получить результат экспорта
            //if (expRes != null)
            //{
            //    MakeLog("expRes: " + Convert.ToString(expRes.PackageID));
            //    Console.WriteLine("expRes: " + Convert.ToString(expRes.PackageID));
            //    MakeLog(expRes.ToString());
            //}
            //else MakeLog("Похоже, что expRes = null");

            //try
            //{
            //    var pakId = expRes.PackageID; //получить номер экспортированного пакета 
            //    MakeLog("Номер импортированного пакета: " + Convert.ToString(pakId));

            //    Console.WriteLine("Номер импортированного пакета: " + Convert.ToString(pakId));
            //    Console.WriteLine("Теперь жди. Программа завершит работу сама, как только получит результаты импорта");
            //    GetImportResultById(pakId);
            //}
            //catch (Exception exception)
            //{
            //    MakeLog(exception.Message);
            //    MakeLog("MainMetod");
            //    if (expRes != null)
            //    {
            //        MakeLog(expRes.ToString());
            //    }
            //    else Console.ReadLine();
            //}
        }

        public override string ToString()
        {
            return "Экспортировать новые и изменившиеся заявления";
        }

        public void Run(Func<string, string> askMore)
        {
            throw new NotImplementedException();
        }
    }
}
