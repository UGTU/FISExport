using System.Collections.Generic;
using System.Linq;
using AbitExportProject.DataDecoders;

using System.Data.Linq;
using System;

namespace AbitExportProject.Data
{
    partial class ABIT_postup
    {
        public const int ZachislState = 2;
        public const int CurrentState = 4;
        public const int NetworkState = 9;
        public const int ReplacedState = 2;
        public const int ZachislAnotherApplicationState = 11;
        public const int NotAuthenticatedState = 3;

        public List<Doc_stud> IdentityDocs => Student.Person.IdentityDocs;       //список идентификационных документов     
        public List<Doc_stud> EducationalDocs => Student.Person.EducationalDocs; //список образовательных документов
        public bool IsCurrent => ABIT_sost_zach.ik_type_zach == CurrentState;    //текущее состояние

        public bool IsZachisl => ABIT_sost_zach.ik_type_zach == ZachislState;    //состояние зачисления

        public bool IsNetwork => _ik_zach == NetworkState;                       //подано по сети
        public bool IsReplaced => _ik_zach == ReplacedState;                     //переведен
        public bool IsZachislAnotherApplication => _ik_zach == ZachislAnotherApplicationState;    //зачислен по другому заявлению

        public bool IsActual => Student.Person.Export_FB_journal.Is_actual;
        public string OriginalReceivedDate => DateTimeDecoder.DateToString(dateOriginal);

        public string RegistrationDate => DateTimeDecoder.DateToString(dd_pod_zayav);

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

        public int StatusId => (IsZachisl) ? CurrentState : (int)(ABIT_sost_zach.ik_FB ?? NotAuthenticatedState);  //Если ik_FB = null, то считаем, что оно не прошло проверку
    }
}
