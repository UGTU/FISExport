using Fdalilib.Actions2015.Errors;

namespace Fdalilib.Actions2015.Logs
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class LogFailedAdmissionVolume {
    
        private TErrorInfo errorInfoField;
    
        private object educationLevelNameField;
    
        private object directionNameField;
    
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
        public object EducationLevelName {
            get {
                return this.educationLevelNameField;
            }
            set {
                this.educationLevelNameField = value;
            }
        }
    
        /// <remarks/>
        public object DirectionName {
            get {
                return this.directionNameField;
            }
            set {
                this.directionNameField = value;
            }
        }
    }
}