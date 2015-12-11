namespace Fdalilib.ImportClasses.Ege
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class EgeDocumentMark {
    
        private string subjectNameField;
    
        private decimal subjectMarkField;
    
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
        public decimal SubjectMark {
            get {
                return this.subjectMarkField;
            }
            set {
                this.subjectMarkField = value;
            }
        }
    }
}