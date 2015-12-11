namespace Fdalilib.XMLCODE.Applications
{

    public partial class Application : ShortApplication
    {

        private PackageDataApplicationEntrant entrantField;

        private System.DateTime lastDenyDateField;

        private bool lastDenyDateFieldSpecified;

        private bool needHostelField;

        private uint statusIDField;

        private string statusCommentField;

        private string[] selectedCompetitiveGroupsField;

        private string[] selectedCompetitiveGroupItemsField;

        private PackageDataApplicationFinSourceEduForm[] finSourceAndEduFormsField;

        private PackageDataApplicationApplicationCommonBenefit applicationCommonBenefitField;

        private PackageDataApplicationApplicationCommonBenefit[] applicationCommonBenefitsField;

        private PackageDataApplicationEntranceTestResult[] entranceTestResultsField;

        private PackageDataApplicationApplicationDocuments applicationDocumentsField;

        /// <remarks/>
        public PackageDataApplicationEntrant Entrant
        {
            get { return this.entrantField; }
            set { this.entrantField = value; }
        }

        /// <remarks/>
        public System.DateTime LastDenyDate
        {
            get { return this.lastDenyDateField; }
            set { this.lastDenyDateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LastDenyDateSpecified
        {
            get { return this.lastDenyDateFieldSpecified; }
            set { this.lastDenyDateFieldSpecified = value; }
        }

        /// <remarks/>
        public bool NeedHostel
        {
            get { return this.needHostelField; }
            set { this.needHostelField = value; }
        }

        /// <remarks/>
        public uint StatusID
        {
            get { return this.statusIDField; }
            set { this.statusIDField = value; }
        }

        /// <remarks/>
        public string StatusComment
        {
            get { return this.statusCommentField; }
            set { this.statusCommentField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CompetitiveGroupID", IsNullable = false)]
        public string[] SelectedCompetitiveGroups
        {
            get { return this.selectedCompetitiveGroupsField; }
            set { this.selectedCompetitiveGroupsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CompetitiveGroupItemID", IsNullable = false)]
        public string[] SelectedCompetitiveGroupItems
        {
            get { return this.selectedCompetitiveGroupItemsField; }
            set { this.selectedCompetitiveGroupItemsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FinSourceEduForm", IsNullable = false)]
        public PackageDataApplicationFinSourceEduForm[] FinSourceAndEduForms
        {
            get { return this.finSourceAndEduFormsField; }
            set { this.finSourceAndEduFormsField = value; }
        }

        /// <remarks/>
        public PackageDataApplicationApplicationCommonBenefit ApplicationCommonBenefit
        {
            get { return this.applicationCommonBenefitField; }
            set { this.applicationCommonBenefitField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ApplicationCommonBenefit", IsNullable = false)]
        public PackageDataApplicationApplicationCommonBenefit[] ApplicationCommonBenefits
        {
            get { return this.applicationCommonBenefitsField; }
            set { this.applicationCommonBenefitsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EntranceTestResult", IsNullable = false)]
        public PackageDataApplicationEntranceTestResult[] EntranceTestResults
        {
            get { return this.entranceTestResultsField; }
            set { this.entranceTestResultsField = value; }
        }

        /// <remarks/>
        public PackageDataApplicationApplicationDocuments ApplicationDocuments
        {
            get { return this.applicationDocumentsField; }
            set { this.applicationDocumentsField = value; }
        }
    }
}