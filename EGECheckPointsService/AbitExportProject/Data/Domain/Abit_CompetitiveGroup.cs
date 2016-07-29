using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbitExportProject.Data
{
    public partial class Abit_CompetitiveGroup
    {
        /// <summary>
        /// уровень обр-я
        /// </summary>
        public DictionaryContent EducationLevel => DictionaryContent;

        /// <summary>
        /// ID их справочника уровня образования (СПО/магистры/бакалавры и т.п.)
        /// </summary>
        public int EducationLevelID => DictionaryContent.IDItem;
        /// <summary>
        /// направление
        /// </summary>
        public SpecDetailDictionary Spec => SpecDetailDictionary;

        /// <summary>
        /// ID их справочника специальностей/направлений
        /// </summary>
        public int SpecID => SpecDetailDictionary.ID;

        /// <summary>
        /// источник фин-я
        /// </summary>
        public DictionaryContent EducSource => DictionaryContent2;

        /// <summary>
        /// ID их справочника источника финансирования
        /// </summary>
        public int EducSourceID => DictionaryContent2.IDItem;

        /// <summary>
        /// форма обучения
        /// </summary>
        public DictionaryContent FormEd => DictionaryContent1;

        /// <summary>
        /// ID их справочника формы обучения
        /// </summary>
        public int FormEdID => DictionaryContent1.IDItem;
    }
}
