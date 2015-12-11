using Fdalilib.XMLCODE.Ege;

namespace Fdalilib.XMLCODE.CheckResults
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", ElementName = "AppSingleCheckResult", IsNullable = false)]
    public partial class AppSingleCheckResult
    {
        private string InstitutionIdField;

        private GetEgeDocument getEgeDocumentsField;

        private EgeDocumentCheckResult egeDocumentCheckResultsField;

        public string InstitutionID 
        {
           get { return this.InstitutionIdField; } 
           set { this.InstitutionIdField = value; }
        }

        [System.Xml.Serialization.XmlElement("GetEgeDocuments", IsNullable = false)]
        public GetEgeDocument GetEgeDocuments
        {
            get { return this.getEgeDocumentsField; }
            set { this.getEgeDocumentsField = value; }
        }

        [System.Xml.Serialization.XmlElement("EgeDocumentCheckResults", IsNullable = false)]
        public EgeDocumentCheckResult EgeDocumentCheckResults
        {
            get { return this.egeDocumentCheckResultsField; }
            set { this.egeDocumentCheckResultsField = value; }
        }
    }
}
