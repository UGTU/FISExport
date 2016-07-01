using Fdalilib.Actions2015.Errors;

namespace Fdalilib.Actions2015.Logs
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class LogFailedEntranceTestResult {
    
        private TErrorInfo errorInfoField;
    
        private string resultSourceTypeField;
    
        private string subjectNameField;
    
        private decimal resultValueField;
    
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
        public string ResultSourceType {
            get {
                return this.resultSourceTypeField;
            }
            set {
                this.resultSourceTypeField = value;
            }
        }
    
        /// <remarks/>
        public string SubjectName {
            get {
                return this.subjectNameField;
            }
            set {
                this.subjectNameField = value;
            }
        }
    
        /// <remarks/>
        public decimal ResultValue {
            get {
                return this.resultValueField;
            }
            set {
                this.resultValueField = value;
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