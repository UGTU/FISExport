using Fdalilib.Actions2015.Errors;

namespace Fdalilib.Actions2015.Logs
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class LogFailedTargetOrganization {
    
        private TErrorInfo errorInfoField;
    
        private string targetOrganizationNameField;
    
        private string competitiveGroupNameField;
    
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
        public string TargetOrganizationName {
            get {
                return this.targetOrganizationNameField;
            }
            set {
                this.targetOrganizationNameField = value;
            }
        }
    
        /// <remarks/>
        public string CompetitiveGroupName {
            get {
                return this.competitiveGroupNameField;
            }
            set {
                this.competitiveGroupNameField = value;
            }
        }
    }
}