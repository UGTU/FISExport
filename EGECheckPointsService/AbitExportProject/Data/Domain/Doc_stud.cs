using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbitExportProject.DataDecoders;

namespace AbitExportProject.Data
{
    public partial class Doc_stud
    {
        public const int MiddleEduDiplomaDocument = 7;
        public const int HighEduDiplomaDocument = 9;
        public const int OtherIdentity = 9;

        public const int VremDocument = 12;
        public string Seria => Cd_seria.Trim();
        public bool IsEmptySeria => (Seria == "");
        public bool IsCorrectData => !Dd_vidan.HasValue||(Dd_vidan < DateTime.Today);
        public string Date => DateTimeDecoder.DateToString(Dd_vidan);
        public string Number => Np_number.Trim();
    }
}
