namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationFinSourceEduForm
    {

        private uint financeSourceIDField;

        private uint educationFormIDField;

        private string targetOrganizationUIDField;

        /// <remarks/>
        public uint FinanceSourceID
        {
            get { return this.financeSourceIDField; }
            set { this.financeSourceIDField = value; }
        }

        /// <remarks/>
        public uint EducationFormID
        {
            get { return this.educationFormIDField; }
            set { this.educationFormIDField = value; }
        }

        /// <remarks/>
        public string TargetOrganizationUID
        {
            get { return this.targetOrganizationUIDField; }
            set { this.targetOrganizationUIDField = value; }
        }
    }
}