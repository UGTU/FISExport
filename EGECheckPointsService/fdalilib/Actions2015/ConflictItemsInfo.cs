//using Fdalilib.XMLCODE.EntranceCampaignStruct;

using System.Collections.Generic;

namespace Fdalilib.Actions2015
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ConflictItemsInfo {
    
        private Application[] _applicationsField;
    
        private Application[] _ordersOfAdmissionField;

        private List<string> _competitiveGroupItemsField;

        private PackageDataApplicationEntranceTestResult _entranceTestResultsField;

        private PackageDataApplicationApplicationCommonBenefit _applicationCommonBenefitsField;
    
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
        public List<string> CompetitiveGroupItems
        {
            get {
                return this._competitiveGroupItemsField;
            }
            set {
                this._competitiveGroupItemsField = value;
            }
        }
    
        /// <remarks/>
        public PackageDataApplicationEntranceTestResult EntranceTestResults
        {
            get {
                return this._entranceTestResultsField;
            }
            set {
                this._entranceTestResultsField = value;
            }
        }
    
        /// <remarks/>
        public PackageDataApplicationApplicationCommonBenefit ApplicationCommonBenefits
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