namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TBasicDiplomaDocument: EducationDocument
    {

        private string uIDField;

       // private System.DateTime originalReceivedDateField;

        private bool originalReceivedDateFieldSpecified;

        private bool documentDateFieldSpecified;

        private string documentOrganizationField;

        private string registrationNumberField;

        private uint qualificationTypeIDField;

        private bool qualificationTypeIDFieldSpecified;

        private uint professionIDField;

        private bool professionIDFieldSpecified;

        private uint endYearField;

        private bool endYearFieldSpecified;

        private bool gPAFieldSpecified;

        /// <remarks/>
        public string UID
        {
            get { return this.uIDField; }
            set { this.uIDField = value; }
        }


      /*  /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime OriginalReceivedDate
        {
            get { return this.originalReceivedDateField; }
            set { this.originalReceivedDateField = value; }
        }*/

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OriginalReceivedDateSpecified
        {
            get { return this.originalReceivedDateFieldSpecified; }
            set { this.originalReceivedDateFieldSpecified = value; }
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
        public string RegistrationNumber
        {
            get { return this.registrationNumberField; }
            set { this.registrationNumberField = value; }
        }

        /// <remarks/>
        public uint QualificationTypeID
        {
            get { return this.qualificationTypeIDField; }
            set { this.qualificationTypeIDField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool QualificationTypeIDSpecified
        {
            get { return this.qualificationTypeIDFieldSpecified; }
            set { this.qualificationTypeIDFieldSpecified = value; }
        }

        /// <remarks/>
        public uint ProfessionID
        {
            get { return this.professionIDField; }
            set { this.professionIDField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ProfessionIDSpecified
        {
            get { return this.professionIDFieldSpecified; }
            set { this.professionIDFieldSpecified = value; }
        }

        /// <remarks/>
        public uint EndYear
        {
            get { return this.endYearField; }
            set { this.endYearField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EndYearSpecified
        {
            get { return this.endYearFieldSpecified; }
            set { this.endYearFieldSpecified = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool GPASpecified
        {
            get { return this.gPAFieldSpecified; }
            set { this.gPAFieldSpecified = value; }
        }
    }
}