namespace Fdalilib.XMLCODE
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PackageDataApplicationApplicationDocuments
    {

        private PackageDataApplicationApplicationDocumentsEgeDocument[] egeDocumentsField;

        private PackageDataApplicationApplicationDocumentsGiaDocument[] giaDocumentsField;

        private PackageDataApplicationApplicationDocumentsIdentityDocument identityDocumentField;

        private PackageDataApplicationApplicationDocumentsEduDocument[] eduDocumentsField;

        private PackageDataApplicationApplicationDocumentsMilitaryCardDocument militaryCardDocumentField;

        private PackageDataApplicationApplicationDocumentsStudentDocument studentDocumentField;

        private TCustomDocument[] customDocumentsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EgeDocument", IsNullable = false)]
        public PackageDataApplicationApplicationDocumentsEgeDocument[] EgeDocuments
        {
            get { return this.egeDocumentsField; }
            set { this.egeDocumentsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("GiaDocument", IsNullable = false)]
        public PackageDataApplicationApplicationDocumentsGiaDocument[] GiaDocuments
        {
            get { return this.giaDocumentsField; }
            set { this.giaDocumentsField = value; }
        }

        /// <remarks/>
        public PackageDataApplicationApplicationDocumentsIdentityDocument IdentityDocument
        {
            get { return this.identityDocumentField; }
            set { this.identityDocumentField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EduDocument", IsNullable = false)]
        public PackageDataApplicationApplicationDocumentsEduDocument[] EduDocuments
        {
            get { return this.eduDocumentsField; }
            set { this.eduDocumentsField = value; }
        }

        /// <remarks/>
        public PackageDataApplicationApplicationDocumentsMilitaryCardDocument MilitaryCardDocument
        {
            get { return this.militaryCardDocumentField; }
            set { this.militaryCardDocumentField = value; }
        }

        /// <remarks/>
        public PackageDataApplicationApplicationDocumentsStudentDocument StudentDocument
        {
            get { return this.studentDocumentField; }
            set { this.studentDocumentField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CustomDocument", IsNullable = false)]
        public TCustomDocument[] CustomDocuments
        {
            get { return this.customDocumentsField; }
            set { this.customDocumentsField = value; }
        }
    }
}