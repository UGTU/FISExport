namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AuthData
    {

        private string loginField;

        private string passField;

        private int institutionIDField;

        private bool institutionIDFieldSpecified = false;

        /// <remarks/>
        public string Login
        {
            get { return this.loginField; }
            set { this.loginField = value; }
        }

        /// <remarks/>
        public string Pass
        {
            get { return this.passField; }
            set { this.passField = value; }
        }

        /// <remarks/>
        public int InstitutionID
        {
            get { return this.institutionIDField; }
            set { this.institutionIDField = value; institutionIDFieldSpecified = true; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InstitutionIDSpecified
        {
            get { return this.institutionIDFieldSpecified; }
            set { this.institutionIDFieldSpecified = value; }
        }
    }
}