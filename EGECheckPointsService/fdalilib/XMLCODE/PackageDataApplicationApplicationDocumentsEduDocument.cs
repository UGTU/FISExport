namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationApplicationDocumentsEduDocument
    {

        private object itemField;

        private ItemChoiceType itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AcademicDiplomaDocument", typeof (TAcademicDiplomaDocument))]
        [System.Xml.Serialization.XmlElementAttribute("BasicDiplomaDocument", typeof (TBasicDiplomaDocument))]
        [System.Xml.Serialization.XmlElementAttribute("EduCustomDocument", typeof (TEduCustomDocument))]
        [System.Xml.Serialization.XmlElementAttribute("HighEduDiplomaDocument", typeof (THighEduDiplomaDocument))]
        [System.Xml.Serialization.XmlElementAttribute("IncomplHighEduDiplomaDocument",
            typeof (TIncomplHighEduDiplomaDocument))]
        [System.Xml.Serialization.XmlElementAttribute("MiddleEduDiplomaDocument", typeof (TMiddleEduDiplomaDocument))]
        [System.Xml.Serialization.XmlElementAttribute("SchoolCertificateBasicDocument",
            typeof (TSchoolCertificateDocument))]
        [System.Xml.Serialization.XmlElementAttribute("SchoolCertificateDocument", typeof (TSchoolCertificateDocument))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public object Item
        {
            get { return this.itemField; }
            set { this.itemField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get { return this.itemElementNameField; }
            set { this.itemElementNameField = value; }
        }
    }
}