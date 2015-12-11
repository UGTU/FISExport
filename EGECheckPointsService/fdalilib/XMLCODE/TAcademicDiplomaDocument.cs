namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TAcademicDiplomaDocument : EducationDocument
    {

        private string uIDField;

        private bool originalReceivedDateFieldSpecified;

        private string registrationNumberField;

        private bool documentDateFieldSpecified;

        private string documentOrganizationField;

        private uint qualificationTypeIDField;

        private bool qualificationTypeIDFieldSpecified;

        private uint specialityIDField;

        private bool specialityIDFieldSpecified;

        private ushort specializationIDField;

        private bool specializationIDFieldSpecified;

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
        public string RegistrationNumber
        {
            get { return this.registrationNumberField; }
            set { this.registrationNumberField = value; }
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
        public uint SpecialityID
        {
            get { return this.specialityIDField; }
            set { this.specialityIDField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SpecialityIDSpecified
        {
            get { return this.specialityIDFieldSpecified; }
            set { this.specialityIDFieldSpecified = value; }
        }

        /// <remarks/>
        public ushort SpecializationID
        {
            get { return this.specializationIDField; }
            set { this.specializationIDField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SpecializationIDSpecified
        {
            get { return this.specializationIDFieldSpecified; }
            set { this.specializationIDFieldSpecified = value; }
        }
    }
}