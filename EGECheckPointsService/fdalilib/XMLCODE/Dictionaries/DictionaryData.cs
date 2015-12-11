using System.Xml.Serialization;

namespace Fdalilib.XMLCODE.Dictionaries
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class DictionaryData
    {

        private int codeField;

        private string nameField;

        private DictionaryItem[] dictionaryItemsField;

        /// <remarks/>
        public int Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DictionaryItem", IsNullable = false)]
        public DictionaryItem[] DictionaryItems
        {
            get
            {
                return this.dictionaryItemsField;
            }
            set
            {
                this.dictionaryItemsField = value;
            }
        }
    }
}
