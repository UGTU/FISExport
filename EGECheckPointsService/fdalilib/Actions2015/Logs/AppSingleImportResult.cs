using Fdalilib.Actions2015.Ege;
using Fdalilib.Actions2015.Errors;

namespace Fdalilib.Actions2015.Logs
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("AppSingleImportResult", Namespace="", IsNullable=false)]
    public partial class AppSingleImportResult {

        private EgeDocumentImportResult[] egeDocumentsField;
    
        private ResultLog logField;
    
        private ResultConflicts conflictsField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EgeDocument", IsNullable=false)]
        public EgeDocumentImportResult[] EgeDocuments
        {
            get {
                return this.egeDocumentsField;
            }
            set {
                this.egeDocumentsField = value;
            }
        }
    
        /// <remarks/>
        public ResultLog Log {
            get {
                return this.logField;
            }
            set {
                this.logField = value;
            }
        }
    
        /// <remarks/>
        public ResultConflicts Conflicts {
            get {
                return this.conflictsField;
            }
            set {
                this.conflictsField = value;
            }
        }
    }
}