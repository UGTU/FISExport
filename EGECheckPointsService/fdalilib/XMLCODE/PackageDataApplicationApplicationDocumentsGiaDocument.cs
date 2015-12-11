namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationApplicationDocumentsGiaDocument
    {

        private string uIDField;

        private bool originalReceivedField;

        private System.DateTime originalReceivedDateField;

        private bool originalReceivedDateFieldSpecified;

        private string documentNumberField;

        private System.DateTime documentDateField;

        private bool documentDateFieldSpecified;

        private string documentOrganizationField;

        private PackageDataApplicationApplicationDocumentsGiaDocumentSubjectData[] subjectsField;

        /// <remarks/>
        public string UID
        {
            get { return this.uIDField; }
            set { this.uIDField = value; }
        }

        /// <remarks/>
        public bool OriginalReceived
        {
            get { return this.originalReceivedField; }
            set { this.originalReceivedField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime OriginalReceivedDate
        {
            get { return this.originalReceivedDateField; }
            set { this.originalReceivedDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OriginalReceivedDateSpecified
        {
            get { return this.originalReceivedDateFieldSpecified; }
            set { this.originalReceivedDateFieldSpecified = value; }
        }

        /// <remarks/>
        public string DocumentNumber
        {
            get { return this.documentNumberField; }
            set { this.documentNumberField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DocumentDate
        {
            get { return this.documentDateField; }
            set { this.documentDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentDateSpecified
        {
            get { return this.documentDateFieldSpecified; }
            set { this.documentDateFieldSpecified = value; }
        }

        /// <remarks/>
        public string DocumentOrganization
        {
            get { return this.documentOrganizationField; }
            set { this.documentOrganizationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("SubjectData", IsNullable = false)]
        public PackageDataApplicationApplicationDocumentsGiaDocumentSubjectData[] Subjects
        {
            get { return this.subjectsField; }
            set { this.subjectsField = value; }
        }
    }
}