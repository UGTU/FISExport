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
        public string Date => DateTimeDecoder.DateToString(Dd_vidan);
        public string Number => Np_number.Trim();
    }

    /// <summary>
    /// Gеречень идентифицирующих документов
    /// </summary>
    public enum IdentityDocuments
    {
        /// <summary>
        /// Свидетельство о рождении
        /// </summary>
        BirthCertificate = 2,
        /// <summary>
        /// Паспорт гражданина РФ
        /// </summary>
        Passport = 4,
        /// <summary>
        /// Военный билет
        /// </summary>
        MilitaryCard = 10,
        /// <summary>
        /// Временное удостоверение личности гражданина РФ
        /// </summary>
        TemporaryIdentityCard = 12,
        /// <summary>
        /// Вид на жительство
        /// </summary>
        ResidencePermit = 15,
        /// <summary>
        /// Паспорт гражданина иностранного государства
        /// </summary>
        AlienPassport = 16,
        /// <summary>
        /// Заграничный паспорт гражданина РФ
        /// </summary>
        InternationalPassport = 44,
        /// <summary>
        /// Дипломатический паспорт гражданина РФ
        /// </summary>
        RussianDiplomaticPassport = 45,
        /// <summary>
        /// Паспорт моряка
        /// </summary>
        SeamanPassport = 46,
        /// <summary>
        /// Свидетельство о рождении, выданное уполномоченным органом иностранного государства
        /// </summary>
        InternationalBirthCertificate = 47,
        /// <summary>
        /// Иной документ, удостоверяющий личность
        /// </summary>
        Another = 48
    }

    /// <summary>
    /// Перечень документов об образовании
    /// </summary>
    public enum EducationalDocuments
    {
        /// <summary>
        /// Аттестат о среднем (полном) общем образовании
        /// </summary>
        SchoolCertificate = 6,
        /// <summary>
        /// Диплом о среднем профессиональном образовании
        /// </summary>
        MiddleEduDiplome = 7,
        /// <summary>
        /// Диплом о начальном профессиональном образовании
        /// </summary>
        BasicDiploma = 8,
        /// <summary>
        /// Диплом о высшем профессиональном образовании
        /// </summary>
        HightEduDiploma = 9,
        /// <summary>
        /// Аттестат об основном общем образовании
        /// </summary>
        SchoolCertificateBasic = 14,
        /// <summary>
        /// Академическая справка
        /// </summary>
        AcademicDiploma = 17,
        /// <summary>
        /// Диплом о неполном высшем профессиональном образовании
        /// </summary>
        IncomplHightEduDiploma = 22,
        /// <summary>
        /// Диплом кандидата наук
        /// </summary>
        PhDDiploma = 23,
        /// <summary>
        /// Диплом об окончании аспирантуры (адъюнкатуры)
        /// </summary>
        PostGraduateDiploma = 24,
        /// <summary>
        /// Иной документ об образовании
        /// </summary>
        Another = 36,
        /// <summary>
        /// Справка об обучении в другом ВУЗе
        /// </summary>
        CertificateOfTrainingAnotherHightSchool = 39,
        /// <summary>
        /// Справка об образовании
        /// </summary>
        CertificateOfEducation = 52,
        /// <summary>
        /// Справка об обучении
        /// </summary>
        CertificateOfTraining = 59,
        
    }
}
