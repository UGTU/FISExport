namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TEntranceTestSubject
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SubjectID", typeof (uint))]
        [System.Xml.Serialization.XmlElementAttribute("SubjectName", typeof (string))]
        public object Item
        {
            get { return this.itemField; }
            set { this.itemField = value; }
        }
    }
}