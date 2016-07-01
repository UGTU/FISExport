
namespace Fdalilib.Actions2015.Errors
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ResultConflicts {
    
        private Application[] _applicationsField;
    
        private Application[] _ordersOfAdmissionField;
    
        private ulong[] _competitiveGroupItemsField;
    
        private uint[] _entranceTestResultsField;

        private PackageDataApplicationApplicationCommonBenefit1 _applicationCommonBenefitsField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Application", IsNullable=false)]
        public Application[] Applications {
            get {
                return this._applicationsField;
            }
            set {
                this._applicationsField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Application", IsNullable=false)]
        public Application[] OrdersOfAdmission {
            get {
                return this._ordersOfAdmissionField;
            }
            set {
                this._ordersOfAdmissionField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CompetitiveGroupItemID", IsNullable=false)]
        public ulong[] CompetitiveGroupItems {
            get {
                return this._competitiveGroupItemsField;
            }
            set {
                this._competitiveGroupItemsField = value;
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EntranceTestsResultID", IsNullable=false)]
        public uint[] EntranceTestResults {
            get {
                return this._entranceTestResultsField;
            }
            set {
                this._entranceTestResultsField = value;
            }
        }
    
        /// <remarks/>
        public PackageDataApplicationApplicationCommonBenefit1 ApplicationCommonBenefits
        {
            get {
                return this._applicationCommonBenefitsField;
            }
            set {
                this._applicationCommonBenefitsField = value;
            }
        }
    }
}