using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.Data;
using Fdalilib.Actions2016.BatchApplicationImport;

namespace AbitExportProject.ActionMethods
{
    /// <summary>
    /// Производит экспорт в ФИС приказов о зачислении
    /// </summary>
    class OrdersImportMethod : BaseProxyMethod<Root, TError, ImportPackageInfo>, IBaseMethod
    {
        public static List<PackageDataOrdersOrderOfAdmission> GetOrdersOfAdmission(UGTUDataDataContext ctx, int year)
        {
            var orders = new List<PackageDataOrdersOrderOfAdmission>();
            foreach (var stud in ctx.Export_FB_journals.Where(x => (x.NNYear == year)))
            {
                if (!ctx.ABIT_postups.ToArray().Any(
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

        public int Year => DateTime.Today.Year;
        protected override string MethodName => "OrdersImportMethod";

        public override string ToString()
        {
            return "Экспортировать приказы о зачислении";
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
                var pack = new PackageData
                {
                    Orders = new PackageDataOrders()
                    {
                        OrdersOfAdmission = GetOrdersOfAdmission(mainCtx, Year)
                    }
                };

                var expRes = proxy.ReturnOrNullAndError(Package, "ImportPack");

                if (expRes == null) return false;
                SavePackNumber(expRes.PackageID);
                return true;
            }
        }
    }
}
