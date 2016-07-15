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
        public DictionaryContent EducationLevel
        {
            get
            {
                return DictionaryContent1;
            }
        }

        /// <summary>
        /// направление
        /// </summary>
        public DictionaryContent Direction
        {
            get
            {
                return DictionaryContent;
            }
        }

        /// <summary>
        /// источник фин-я
        /// </summary>
        public DictionaryContent EducSource
        {
            get
            {
                return DictionaryContent3;
            }
        }

        /// <summary>
        /// форма обучения
        /// </summary>
        public DictionaryContent FormEd
        {
            get
            {
                return DictionaryContent2;
            }
        }
    }
}
