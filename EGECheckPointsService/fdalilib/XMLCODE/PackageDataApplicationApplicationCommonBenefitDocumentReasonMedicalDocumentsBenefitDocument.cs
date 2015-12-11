namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationApplicationCommonBenefitDocumentReasonMedicalDocumentsBenefitDocument
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DisabilityDocument", typeof (TDisabilityDocument))]
        [System.Xml.Serialization.XmlElementAttribute("MedicalDocument", typeof (TMedicalDocument))]
        public object Item
        {
            get { return this.itemField; }
            set { this.itemField = value; }
        }
    }
}