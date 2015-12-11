namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TOlympicTotalDocument
    {

        private string uIDField;

        private bool originalReceivedField;

        private System.DateTime originalReceivedDateField;

        private bool originalReceivedDateFieldSpecified;

        private string documentSeriesField;

        private string documentNumberField;

        private string olympicPlaceField;

        private System.DateTime olympicDateField;

        private bool olympicDateFieldSpecified;

        private uint diplomaTypeIDField;

        private TOlympicTotalDocumentSubjectBriefData[] subjectsField;

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
        public string OlympicPlace
        {
            get { return this.olympicPlaceField; }
            set { this.olympicPlaceField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime OlympicDate
        {
            get { return this.olympicDateField; }
            set { this.olympicDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OlympicDateSpecified
        {
            get { return this.olympicDateFieldSpecified; }
            set { this.olympicDateFieldSpecified = value; }
        }

        /// <remarks/>
        public uint DiplomaTypeID
        {
            get { return this.diplomaTypeIDField; }
            set { this.diplomaTypeIDField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("SubjectBriefData", IsNullable = false)]
        public TOlympicTotalDocumentSubjectBriefData[] Subjects
        {
            get { return this.subjectsField; }
            set { this.subjectsField = value; }
        }
    }
}