namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationApplicationCommonBenefitDocumentReasonMedicalDocuments
    {

        private PackageDataApplicationApplicationCommonBenefitDocumentReasonMedicalDocumentsBenefitDocument
            benefitDocumentField;

        private TAllowEducationDocument allowEducationDocumentField;

        /// <remarks/>
        public PackageDataApplicationApplicationCommonBenefitDocumentReasonMedicalDocumentsBenefitDocument
            BenefitDocument
        {
            get { return this.benefitDocumentField; }
            set { this.benefitDocumentField = value; }
        }

        /// <remarks/>
        public TAllowEducationDocument AllowEducationDocument
        {
            get { return this.allowEducationDocumentField; }
            set { this.allowEducationDocumentField = value; }
        }
    }
}