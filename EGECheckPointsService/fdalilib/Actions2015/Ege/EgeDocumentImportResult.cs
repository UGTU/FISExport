namespace Fdalilib.Actions2015.Ege
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class EgeDocumentImportResult {
    
        private uint statusCodeField;
    
        private string statusMessageField;
    
        private string documentNumberField;
    
        private System.DateTime documentDateField;
    
        private EgeDocumentImportCorrectResult[] correctResultsField;
    
        private EgeDocumentImportIncorrectResult[] incorrectResultsField;
    
        /// <remarks/>
        public uint StatusCode {
            get {
                return this.statusCodeField;
            }
            set {
                this.statusCodeField = value;
            }
        }
    
        /// <remarks/>
        public string StatusMessage {
            get {
                return this.statusMessageField;
            }
            set {
                this.statusMessageField = value;
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
        public System.DateTime DocumentDate {
            get {
                return this.documentDateField;
            }
            set {
                this.documentDateField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CorrectResultItem", IsNullable=false)]
        public EgeDocumentImportCorrectResult[] CorrectResults {
            get {
                return this.correctResultsField;
            }
            set {
                this.correctResultsField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("IncorrectResultItem", IsNullable=false)]
        public EgeDocumentImportIncorrectResult[] IncorrectResults {
            get {
                return this.incorrectResultsField;
            }
            set {
                this.incorrectResultsField = value;
            }
        }
    }
}