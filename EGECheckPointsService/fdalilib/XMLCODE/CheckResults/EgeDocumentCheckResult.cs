using Fdalilib.ImportClasses;
//using Fdalilib.XMLCODE.Applications;
using Fdalilib.XMLCODE.Ege;

namespace Fdalilib.XMLCODE.CheckResults
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class EgeDocumentCheckResult {
    
        private ShortApplication _applicationField;

        private EgeDocumentImportResult[] _egeDocumentsField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Application", IsNullable = true)]
        public ShortApplication Application {
            get {
                return this._applicationField;
            }
            set {
                this._applicationField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EgeDocument", IsNullable=false)]
        public EgeDocumentImportResult[] EgeDocuments
        {
            get {
                return this._egeDocumentsField;
            }
            set {
                this._egeDocumentsField = value;
            }
        }
    }
}