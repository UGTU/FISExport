namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TInstitutionDocument
    {

        private string documentNumberField;

        private System.DateTime documentDateField;

        private bool documentDateFieldSpecified;

        private uint documentTypeIDField;

        private bool documentTypeIDFieldSpecified;

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
    }
}