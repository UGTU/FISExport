namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationApplicationCommonBenefit1
    {

        private string uIDField;

        private string competitiveGroupIDField;

        private uint documentTypeIDField;

        private bool documentTypeIDFieldSpecified;

        private PackageDataApplicationApplicationCommonBenefitDocumentReason1 documentReasonField;

        private uint benefitKindIDField;

        /// <remarks/>
        public string UID
        {
            get { return this.uIDField; }
            set { this.uIDField = value; }
        }

        /// <remarks/>
        public string CompetitiveGroupID
        {
            get { return this.competitiveGroupIDField; }
            set { this.competitiveGroupIDField = value; }
        }

        /// <remarks/>
        public uint DocumentTypeID
        {
            get { return this.documentTypeIDField; }
            set { this.documentTypeIDField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentTypeIDSpecified
        {
            get { return this.documentTypeIDFieldSpecified; }
            set { this.documentTypeIDFieldSpecified = value; }
        }

        /// <remarks/>
        public PackageDataApplicationApplicationCommonBenefitDocumentReason1 DocumentReason
        {
            get { return this.documentReasonField; }
            set { this.documentReasonField = value; }
        }

        /// <remarks/>
        public uint BenefitKindID
        {
            get { return this.benefitKindIDField; }
            set { this.benefitKindIDField = value; }
        }
    }
}