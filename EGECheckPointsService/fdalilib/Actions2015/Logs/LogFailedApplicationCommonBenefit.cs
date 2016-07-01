using Fdalilib.Actions2015.Errors;

namespace Fdalilib.Actions2015.Logs
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class LogFailedApplicationCommonBenefit {
    
        private TErrorInfo errorInfoField;
    
        private string benefitKindNameField;
    
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
        public string BenefitKindName {
            get {
                return this.benefitKindNameField;
            }
            set {
                this.benefitKindNameField = value;
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