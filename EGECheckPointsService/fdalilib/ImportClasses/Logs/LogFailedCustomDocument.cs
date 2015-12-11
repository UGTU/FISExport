using Fdalilib.ImportClasses.Errors;

namespace Fdalilib.ImportClasses.Logs
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class LogFailedCustomDocument {
    
        private TErrorInfo errorInfoField;
    
        private string documentNumberField;
    
        private System.DateTime documentDateField;
    
        private bool documentDateFieldSpecified;
    
        private string applicationNumberField;
    
        private System.DateTime registrationDateField;
    
        /// <remarks/>
        public TErrorInfo ErrorInfo {
            get {
                return this.errorInfoField;
            }
            set {
                this.errorInfoField = value;
            }
        }
    
        /// <remarks/>
        public string DocumentNumber {
            get {
                return this.documentNumberField;
            }
            set {
                this.documentNumberField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime DocumentDate {
            get {
                return this.documentDateField;
            }
            set {
                this.documentDateField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentDateSpecified {
            get {
                return this.documentDateFieldSpecified;
            }
            set {
                this.documentDateFieldSpecified = value;
            }
        }
    
        /// <remarks/>
        public string ApplicationNumber {
            get {
                return this.applicationNumberField;
            }
            set {
                this.applicationNumberField = value;
            }
        }
    
        /// <remarks/>
        public System.DateTime RegistrationDate {
            get {
                return this.registrationDateField;
            }
            set {
                this.registrationDateField = value;
            }
        }
    }
}