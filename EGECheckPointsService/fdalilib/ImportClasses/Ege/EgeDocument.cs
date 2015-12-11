namespace Fdalilib.ImportClasses.Ege
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class EgeDocument {
    
        private string certificateNumberField;
    
        private string typographicNumberField;
    
        private uint yearField;
    
        private string statusField;
    
        private EgeDocumentMark[] marksField;
    
        /// <remarks/>
        public string CertificateNumber {
            get {
                return this.certificateNumberField;
            }
            set {
                this.certificateNumberField = value;
            }
        }
    
        /// <remarks/>
        public string TypographicNumber {
            get {
                return this.typographicNumberField;
            }
            set {
                this.typographicNumberField = value;
            }
        }
    
        /// <remarks/>
        public uint Year {
            get {
                return this.yearField;
            }
            set {
                this.yearField = value;
            }
        }
    
        /// <remarks/>
        public string Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Mark", IsNullable=false)]
        public EgeDocumentMark[] Marks {
            get {
                return this.marksField;
            }
            set {
                this.marksField = value;
            }
        }
    }
}