using System;

namespace Fdalilib.XMLCODE
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class CheckApp
    {

        private DateTime registrationDateField;

        private string applicationNumberField;

        /// <remarks/>
        public DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }
    }

    public partial class Root
    {

        private CheckApp checkAppField;


        public CheckApp CheckApp
        {
            get
            {
                return this.checkAppField;
            }
            set
            {
                this.checkAppField = value;
            }
        }

    }
}