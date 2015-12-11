
using System.Xml.Serialization;

namespace Fdalilib.ImportClasses
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Dictionary
    {

        private uint codeField;

        private string nameField;

        /// <remarks/>
        public uint Code
        {
            get { return this.codeField; }
            set { this.codeField = value; }
        }

        /// <remarks/>
        public string Name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }
    }
}
