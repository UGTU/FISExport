namespace Fdalilib.Actions2015
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]

    public class ShortApplication
    {
        private string _uIdField;

        private string _applicationNumberField;

        private System.DateTime _registrationDateField;

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this._applicationNumberField;
            }
            set
            {
                this._applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this._registrationDateField;
            }
            set
            {
                this._registrationDateField = value;
            }
        }

        public string Uid
        {
            get
            {
                return this._uIdField;
            }
            set
            {
                this._uIdField = value;
            }
        }
    }
    
    
}