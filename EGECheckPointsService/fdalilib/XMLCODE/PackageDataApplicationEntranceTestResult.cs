namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationEntranceTestResult
    {

        private string uIDField;

        private decimal resultValueField;

        private uint resultSourceTypeIDField;

        private TEntranceTestSubject entranceTestSubjectField;

        private uint entranceTestTypeIDField;

        private string competitiveGroupIDField;

        private PackageDataApplicationEntranceTestResultResultDocument resultDocumentField;

        /// <remarks/>
        public string UID
        {
            get { return this.uIDField; }
            set { this.uIDField = value; }
        }

        /// <remarks/>
        public decimal ResultValue
        {
            get { return this.resultValueField; }
            set { this.resultValueField = value; }
        }

        /// <remarks/>
        public uint ResultSourceTypeID
        {
            get { return this.resultSourceTypeIDField; }
            set { this.resultSourceTypeIDField = value; }
        }

        /// <remarks/>
        public TEntranceTestSubject EntranceTestSubject
        {
            get { return this.entranceTestSubjectField; }
            set { this.entranceTestSubjectField = value; }
        }

        /// <remarks/>
        public uint EntranceTestTypeID
        {
            get { return this.entranceTestTypeIDField; }
            set { this.entranceTestTypeIDField = value; }
        }

        /// <remarks/>
        public string CompetitiveGroupID
        {
            get { return this.competitiveGroupIDField; }
            set { this.competitiveGroupIDField = value; }
        }

        /// <remarks/>
        public PackageDataApplicationEntranceTestResultResultDocument ResultDocument
        {
            get { return this.resultDocumentField; }
            set { this.resultDocumentField = value; }
        }
    }
}