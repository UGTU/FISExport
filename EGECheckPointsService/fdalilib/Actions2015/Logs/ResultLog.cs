namespace Fdalilib.Actions2015.Logs
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ResultLog {
    
        private LogSuccessful successfulField;
    
        private LogFailed failedField;
    
        /// <remarks/>
        public LogSuccessful Successful {
            get {
                return this.successfulField;
            }
            set {
                this.successfulField = value;
            }
        }
    
        /// <remarks/>
        public LogFailed Failed {
            get {
                return this.failedField;
            }
            set {
                this.failedField = value;
            }
        }
    }
}