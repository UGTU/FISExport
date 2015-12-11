using System.Xml.Serialization;

namespace Fdalilib.XMLCODE.Dictionaries
{
    public partial class DictionaryItem
    {

        private string codeField;

        private string qualificationCodeField;

        private string periodField;

        private string uGSCodeField;

        private string uGSNameField;


        /// <remarks/>
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public string QualificationCode
        {
            get
            {
                return this.qualificationCodeField;
            }
            set
            {
                this.qualificationCodeField = value;
            }
        }

        /// <remarks/>
        public string Period
        {
            get
            {
                return this.periodField;
            }
            set
            {
                this.periodField = value;
            }
        }

        /// <remarks/>
        public string UGSCode
        {
            get
            {
                return this.uGSCodeField;
            }
            set
            {
                this.uGSCodeField = value;
            }
        }

        /// <remarks/>
        public string UGSName
        {
            get
            {
                return this.uGSNameField;
            }
            set
            {
                this.uGSNameField = value;
            }
        }
    }
}
