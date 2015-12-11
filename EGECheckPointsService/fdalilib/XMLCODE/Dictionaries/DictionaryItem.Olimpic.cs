using System.Xml.Serialization;

namespace Fdalilib.XMLCODE.Dictionaries
{
    public partial class DictionaryItem
    {

        private uint olympicIDField;

        private uint olympicNumberField;

        private string olympicNameField;

        private uint olympicLevelIDField;

        private uint[] subjectsField;

        /// <remarks/>
        public uint OlympicID
        {
            get
            {
                return this.olympicIDField;
            }
            set
            {
                this.olympicIDField = value;
            }
        }

        /// <remarks/>
        public uint OlympicNumber
        {
            get
            {
                return this.olympicNumberField;
            }
            set
            {
                this.olympicNumberField = value;
            }
        }

        /// <remarks/>
        public string OlympicName
        {
            get
            {
                return this.olympicNameField;
            }
            set
            {
                this.olympicNameField = value;
            }
        }

        /// <remarks/>
        public uint OlympicLevelID
        {
            get
            {
                return this.olympicLevelIDField;
            }
            set
            {
                this.olympicLevelIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("SubjectID", IsNullable = false)]
        public uint[] Subjects
        {
            get
            {
                return this.subjectsField;
            }
            set
            {
                this.subjectsField = value;
            }
        }
    }

}
