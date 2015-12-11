using System.Collections.Generic;
using System.Linq;

namespace AbitExportProject.Data
{
    partial class ABIT_postup
    {
        public int SpecIk
        {

            get
            {
                //если бакалавры, магистры или специалитет
                if (ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_FB.HasValue)
                    return ABIT_Diapazon_spec_fac.Relation_spec_fac.ik_spec;
                else
                {
                    return (int)ABIT_Diapazon_spec_fac.Relation_spec_fac.EducationBranch.ik_main_spec;
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
