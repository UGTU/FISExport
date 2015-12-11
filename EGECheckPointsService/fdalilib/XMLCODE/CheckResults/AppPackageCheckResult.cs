using Fdalilib.XMLCODE.Ege;

namespace Fdalilib.XMLCODE.CheckResults
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class AppPackageCheckResult {
    
        private ushort packageIDField;
    
        private uint statusCheckCodeField;
    
        private string statusCheckMessageField;
    
        private EgeDocumentCheckResult[] egeDocumentCheckResultsField;
    
        private GetEgeDocument[] getEgeDocumentsField;
    
        /// <remarks/>
        public ushort PackageID {
            get {
                return this.packageIDField;
            }
            set {
                this.packageIDField = value;
            }
        }
    
        /// <remarks/>
        public uint StatusCheckCode {
            get {
                return this.statusCheckCodeField;
            }
            set {
                this.statusCheckCodeField = value;
            }
        }
    
        /// <remarks/>
        public string StatusCheckMessage {
            get {
                return this.statusCheckMessageField;
            }
            set {
                this.statusCheckMessageField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EgeDocumentCheckResult", IsNullable=false)]
        public EgeDocumentCheckResult[] EgeDocumentCheckResults {
            get {
                return this.egeDocumentCheckResultsField;
            }
            set {
                this.egeDocumentCheckResultsField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("GetEgeDocument", IsNullable=false)]
        public GetEgeDocument[] GetEgeDocuments {
            get {
                return this.getEgeDocumentsField;
            }
            set {
                this.getEgeDocumentsField = value;
            }
        }
    }
}