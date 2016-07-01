using System.Collections.Generic;
using System.Linq;
using AbitExportProject.DataDecoders;

namespace AbitExportProject.Data
{
    partial class ABIT_postup
    {
        const int ZachislState = 2;
        const int CurrentState = 4;
        public List<Doc_stud> IdentityDocs => Student.Person.IdentityDocs;       //список идентификационных документов     
        public List<Doc_stud> EducationalDocs => Student.Person.EducationalDocs; //список образовательных документов
        public bool IsCurrent => ABIT_sost_zach.ik_type_zach == CurrentState;    //текущее состояние
        public bool IsZachisl => ABIT_sost_zach.ik_type_zach == ZachislState;    //состояние зачисления



        public bool IsActual => Student.Person.Export_FB_journal.Is_actual;
        public string OriginalReceivedDate => DateTimeDecoder.DateToString(dateOriginal);


        public int SpecIk
        {

            get
            {
                //если бакалавры, магистры или специалитет
                if (ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_FB.HasValue)
                    return ABIT_Diapazon_spec_fac.Relation_spec_fac.ik_spec;
                else
                {
                    return (int) ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_main_spec;
                }

            }
        }

        public int DirectionId
        {
            get
            {
                var mContext = new UGTUDataDataContext();
                var spik = SpecIk;
                return mContext.EducationBranches.Single(x => x.ik_spec == spik).ik_FB ?? 0;
            }

        }

        
    }

}
