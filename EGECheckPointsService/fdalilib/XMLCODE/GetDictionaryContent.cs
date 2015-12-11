using System.Xml.Serialization;

namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(TypeName="Xml")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class GetDictionaryContent
    {

        private int dictionaryCodeField;

        /// <remarks/>
        public int DictionaryCode
        {
            get
            {
                return this.dictionaryCodeField;
            }
            set
            {
                this.dictionaryCodeField = value;
            }
        }
    }
}