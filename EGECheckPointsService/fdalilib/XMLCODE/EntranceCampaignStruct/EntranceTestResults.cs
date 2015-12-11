namespace Fdalilib.XMLCODE.EntranceCampaignStruct
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class EntranceTestResults {
    
        private uint[] entranceTestsResultIDField;
    
        private string uIDField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EntranceTestsResultID")]
        public uint[] EntranceTestsResultID {
            get {
                return this.entranceTestsResultIDField;
            }
            set {
                this.entranceTestsResultIDField = value;
            }
        }
    
        /// <remarks/>
        public string UID {
            get {
                return this.uIDField;
            }
            set {
                this.uIDField = value;
            }
        }
    }
}