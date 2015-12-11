namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationEntranceTestResultResultDocument
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EgeDocumentID", typeof (string))]
        [System.Xml.Serialization.XmlElementAttribute("InstitutionDocument", typeof (TInstitutionDocument))]
        [System.Xml.Serialization.XmlElementAttribute("OlympicDocument", typeof (TOlympicDocument))]
        [System.Xml.Serialization.XmlElementAttribute("OlympicTotalDocument", typeof (TOlympicTotalDocument))]
        public object Item
        {
            get { return this.itemField; }
            set { this.itemField = value; }
        }
    }
}