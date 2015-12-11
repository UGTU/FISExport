namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TSchoolCertificateDocument : EducationDocument
    {

        private string uIDField;

        private bool originalReceivedDateFieldSpecified;


        private bool documentDateFieldSpecified;

        private string documentOrganizationField;

        private uint endYearField;

        private bool endYearFieldSpecified;

        

        private bool gPAFieldSpecified;

        /// <remarks/>
        public string UID
        {
            get { return this.uIDField; }
            set { this.uIDField = value; }
        }


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