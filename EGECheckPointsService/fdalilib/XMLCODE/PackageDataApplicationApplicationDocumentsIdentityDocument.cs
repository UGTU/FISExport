namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationApplicationDocumentsIdentityDocument
    {

        private string uIDField;

        private bool originalReceivedField;

        private System.DateTime originalReceivedDateField;

        private bool originalReceivedDateFieldSpecified;

        private string documentSeriesField;

        private string documentNumberField;

        private string subdivisionCodeField;

        private System.DateTime documentDateField;

        private string documentOrganizationField;

        private uint identityDocumentTypeIDField;

        private uint nationalityTypeIDField;

        private System.DateTime birthDateField;

        private string birthPlaceField;

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
        public string DocumentSeries
        {
            get { return this.documentSeriesField; }
            set { this.documentSeriesField = value; }
        }

        /// <remarks/>
        public string DocumentNumber
        {
            get { return this.documentNumberField; }
            set { this.documentNumberField = value; }
        }

        /// <remarks/>
        public string SubdivisionCode
        {
            get { return this.subdivisionCodeField; }
            set { this.subdivisionCodeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DocumentDate
        {
            get { return this.documentDateField; }
            set { this.documentDateField = value; }
        }

        /// <remarks/>
        public string DocumentOrganization
        {
            get { return this.documentOrganizationField; }
            set { this.documentOrganizationField = value; }
        }

        /// <remarks/>
        public uint IdentityDocumentTypeID
        {
            get { return this.identityDocumentTypeIDField; }
            set { this.identityDocumentTypeIDField = value; }
        }

        /// <remarks/>
        public uint NationalityTypeID
        {
            get { return this.nationalityTypeIDField; }
            set { this.nationalityTypeIDField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime BirthDate
        {
            get { return this.birthDateField; }
            set { this.birthDateField = value; }
        }

        /// <remarks/>
        public string BirthPlace
        {
            get { return this.birthPlaceField; }
            set { this.birthPlaceField = value; }
        }
    }
}