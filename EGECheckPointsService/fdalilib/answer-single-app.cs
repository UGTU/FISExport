//using Fdalilib.XMLCODE.Applications;
//using System.Xml.Serialization;

// 
// Этот исходный код был создан с помощью xsd, версия=4.0.30319.17929.
// 

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ImportResultPackage
    {

        private uint packageIDField;

        private ImportResultPackageLog logField;

        private ImportResultPackageConflicts conflictsField;

        /// <remarks/>
        public uint PackageID
        {
            get
            {
                return this.packageIDField;
            }
            set
            {
                this.packageIDField = value;
            }
        }

        /// <remarks/>
        public ImportResultPackageLog Log
        {
            get
            {
                return this.logField;
            }
            set
            {
                this.logField = value;
            }
        }

        /// <remarks/>
        public ImportResultPackageConflicts Conflicts
        {
            get
            {
                return this.conflictsField;
            }
            set
            {
                this.conflictsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLog
    {

        private ImportResultPackageLogSuccessful successfulField;

        private ImportResultPackageLogFailed failedField;

        /// <remarks/>
        public ImportResultPackageLogSuccessful Successful
        {
            get
            {
                return this.successfulField;
            }
            set
            {
                this.successfulField = value;
            }
        }

        /// <remarks/>
        public ImportResultPackageLogFailed Failed
        {
            get
            {
                return this.failedField;
            }
            set
            {
                this.failedField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogSuccessful
    {

        private uint applicationsField;

        /// <remarks/>
        public uint Applications
        {
            get
            {
                return this.applicationsField;
            }
            set
            {
                this.applicationsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TErrorInfo
    {

        private uint errorCodeField;

        private string messageField;

        private TErrorInfoConflictItemsInfo conflictItemsInfoField;

        /// <remarks/>
        public uint ErrorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }

        /// <remarks/>
        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        /// <remarks/>
        public TErrorInfoConflictItemsInfo ConflictItemsInfo
        {
            get
            {
                return this.conflictItemsInfoField;
            }
            set
            {
                this.conflictItemsInfoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TErrorInfoConflictItemsInfo
    {

        private TErrorInfoConflictItemsInfoApplication[] applicationsField;

        private TErrorInfoConflictItemsInfoEntranceTestResults entranceTestResultsField;

        private TErrorInfoConflictItemsInfoApplicationCommonBenefits applicationCommonBenefitsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Application", IsNullable = false)]
        public TErrorInfoConflictItemsInfoApplication[] Applications
        {
            get
            {
                return this.applicationsField;
            }
            set
            {
                this.applicationsField = value;
            }
        }

        /// <remarks/>
        public TErrorInfoConflictItemsInfoEntranceTestResults EntranceTestResults
        {
            get
            {
                return this.entranceTestResultsField;
            }
            set
            {
                this.entranceTestResultsField = value;
            }
        }

        /// <remarks/>
        public TErrorInfoConflictItemsInfoApplicationCommonBenefits ApplicationCommonBenefits
        {
            get
            {
                return this.applicationCommonBenefitsField;
            }
            set
            {
                this.applicationCommonBenefitsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TErrorInfoConflictItemsInfoApplication
    {

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        private string uIDField;

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }

        /// <remarks/>
        public string UID
        {
            get
            {
                return this.uIDField;
            }
            set
            {
                this.uIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TErrorInfoConflictItemsInfoEntranceTestResults
    {

        private uint[] entranceTestsResultIDField;

        private string uIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EntranceTestsResultID")]
        public uint[] EntranceTestsResultID
        {
            get
            {
                return this.entranceTestsResultIDField;
            }
            set
            {
                this.entranceTestsResultIDField = value;
            }
        }

        /// <remarks/>
        public string UID
        {
            get
            {
                return this.uIDField;
            }
            set
            {
                this.uIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TErrorInfoConflictItemsInfoApplicationCommonBenefits
    {

        private uint applicationCommonBenefitIDField;

        private string uIDField;

        /// <remarks/>
        public uint ApplicationCommonBenefitID
        {
            get
            {
                return this.applicationCommonBenefitIDField;
            }
            set
            {
                this.applicationCommonBenefitIDField = value;
            }
        }

        /// <remarks/>
        public string UID
        {
            get
            {
                return this.uIDField;
            }
            set
            {
                this.uIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailed
    {

        private ImportResultPackageLogFailedEntranceTestItem[] entranceTestItemsField;

        private ImportResultPackageLogFailedCommonBenefitItem[] commonBenefitField;

        private ImportResultPackageLogFailedEntranceTestBenefitItem[] entranceTestBenefitsField;

        private ImportResultPackageLogFailedApplication[] applicationsField;

        private ImportResultPackageLogFailedOlympicDocument[] olympicDocumentsField;

        private ImportResultPackageLogFailedOlympicTotalDocument[] olympicTotalDocumentsField;

        private ImportResultPackageLogFailedDisabilityDocument[] disabilityDocumentsField;

        private ImportResultPackageLogFailedMedicalDocument[] medicalDocumentsField;

        private ImportResultPackageLogFailedAllowEducationDocument[] allowEducationDocumentsField;

        private ImportResultPackageLogFailedCustomDocument[] customDocumentsField;

        private ImportResultPackageLogFailedEntranceTestResult[] entranceTestResultsField;

        private ImportResultPackageLogFailedApplicationCommonBenefit[] applicationCommonBenefitsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EntranceTestItem", IsNullable = false)]
        public ImportResultPackageLogFailedEntranceTestItem[] EntranceTestItems
        {
            get
            {
                return this.entranceTestItemsField;
            }
            set
            {
                this.entranceTestItemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CommonBenefitItem", IsNullable = false)]
        public ImportResultPackageLogFailedCommonBenefitItem[] CommonBenefit
        {
            get
            {
                return this.commonBenefitField;
            }
            set
            {
                this.commonBenefitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EntranceTestBenefitItem", IsNullable = false)]
        public ImportResultPackageLogFailedEntranceTestBenefitItem[] EntranceTestBenefits
        {
            get
            {
                return this.entranceTestBenefitsField;
            }
            set
            {
                this.entranceTestBenefitsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Application", IsNullable = false)]
        public ImportResultPackageLogFailedApplication[] Applications
        {
            get
            {
                return this.applicationsField;
            }
            set
            {
                this.applicationsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("OlympicDocument", IsNullable = false)]
        public ImportResultPackageLogFailedOlympicDocument[] OlympicDocuments
        {
            get
            {
                return this.olympicDocumentsField;
            }
            set
            {
                this.olympicDocumentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("OlympicTotalDocument", IsNullable = false)]
        public ImportResultPackageLogFailedOlympicTotalDocument[] OlympicTotalDocuments
        {
            get
            {
                return this.olympicTotalDocumentsField;
            }
            set
            {
                this.olympicTotalDocumentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DisabilityDocument", IsNullable = false)]
        public ImportResultPackageLogFailedDisabilityDocument[] DisabilityDocuments
        {
            get
            {
                return this.disabilityDocumentsField;
            }
            set
            {
                this.disabilityDocumentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("MedicalDocument", IsNullable = false)]
        public ImportResultPackageLogFailedMedicalDocument[] MedicalDocuments
        {
            get
            {
                return this.medicalDocumentsField;
            }
            set
            {
                this.medicalDocumentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("AllowEducationDocument", IsNullable = false)]
        public ImportResultPackageLogFailedAllowEducationDocument[] AllowEducationDocuments
        {
            get
            {
                return this.allowEducationDocumentsField;
            }
            set
            {
                this.allowEducationDocumentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CustomDocument", IsNullable = false)]
        public ImportResultPackageLogFailedCustomDocument[] CustomDocuments
        {
            get
            {
                return this.customDocumentsField;
            }
            set
            {
                this.customDocumentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EntranceTestResult", IsNullable = false)]
        public ImportResultPackageLogFailedEntranceTestResult[] EntranceTestResults
        {
            get
            {
                return this.entranceTestResultsField;
            }
            set
            {
                this.entranceTestResultsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ApplicationCommonBenefit", IsNullable = false)]
        public ImportResultPackageLogFailedApplicationCommonBenefit[] ApplicationCommonBenefits
        {
            get
            {
                return this.applicationCommonBenefitsField;
            }
            set
            {
                this.applicationCommonBenefitsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedEntranceTestItem
    {

        private TErrorInfo errorInfoField;

        private string entranceTestTypeField;

        private string subjectNameField;

        private string competitiveGroupNameField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string EntranceTestType
        {
            get
            {
                return this.entranceTestTypeField;
            }
            set
            {
                this.entranceTestTypeField = value;
            }
        }

        /// <remarks/>
        public string SubjectName
        {
            get
            {
                return this.subjectNameField;
            }
            set
            {
                this.subjectNameField = value;
            }
        }

        /// <remarks/>
        public string CompetitiveGroupName
        {
            get
            {
                return this.competitiveGroupNameField;
            }
            set
            {
                this.competitiveGroupNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedCommonBenefitItem
    {

        private TErrorInfo errorInfoField;

        private string benefitKindNameField;

        private string competitiveGroupNameField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string BenefitKindName
        {
            get
            {
                return this.benefitKindNameField;
            }
            set
            {
                this.benefitKindNameField = value;
            }
        }

        /// <remarks/>
        public string CompetitiveGroupName
        {
            get
            {
                return this.competitiveGroupNameField;
            }
            set
            {
                this.competitiveGroupNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedEntranceTestBenefitItem
    {

        private TErrorInfo errorInfoField;

        private string benefitKindNameField;

        private string entranceTestTypeField;

        private string subjectNameField;

        private string competitiveGroupNameField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string BenefitKindName
        {
            get
            {
                return this.benefitKindNameField;
            }
            set
            {
                this.benefitKindNameField = value;
            }
        }

        /// <remarks/>
        public string EntranceTestType
        {
            get
            {
                return this.entranceTestTypeField;
            }
            set
            {
                this.entranceTestTypeField = value;
            }
        }

        /// <remarks/>
        public string SubjectName
        {
            get
            {
                return this.subjectNameField;
            }
            set
            {
                this.subjectNameField = value;
            }
        }

        /// <remarks/>
        public string CompetitiveGroupName
        {
            get
            {
                return this.competitiveGroupNameField;
            }
            set
            {
                this.competitiveGroupNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedApplication
    {

        private TErrorInfo errorInfoField;

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedOlympicDocument
    {

        private TErrorInfo errorInfoField;

        private string documentNumberField;

        private System.DateTime documentDateField;

        private bool documentDateFieldSpecified;

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string DocumentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DocumentDate
        {
            get
            {
                return this.documentDateField;
            }
            set
            {
                this.documentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentDateSpecified
        {
            get
            {
                return this.documentDateFieldSpecified;
            }
            set
            {
                this.documentDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedOlympicTotalDocument
    {

        private TErrorInfo errorInfoField;

        private string documentNumberField;

        private System.DateTime documentDateField;

        private bool documentDateFieldSpecified;

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string DocumentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DocumentDate
        {
            get
            {
                return this.documentDateField;
            }
            set
            {
                this.documentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentDateSpecified
        {
            get
            {
                return this.documentDateFieldSpecified;
            }
            set
            {
                this.documentDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedDisabilityDocument
    {

        private TErrorInfo errorInfoField;

        private string documentNumberField;

        private System.DateTime documentDateField;

        private bool documentDateFieldSpecified;

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string DocumentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DocumentDate
        {
            get
            {
                return this.documentDateField;
            }
            set
            {
                this.documentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentDateSpecified
        {
            get
            {
                return this.documentDateFieldSpecified;
            }
            set
            {
                this.documentDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedMedicalDocument
    {

        private TErrorInfo errorInfoField;

        private string documentNumberField;

        private System.DateTime documentDateField;

        private bool documentDateFieldSpecified;

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string DocumentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DocumentDate
        {
            get
            {
                return this.documentDateField;
            }
            set
            {
                this.documentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentDateSpecified
        {
            get
            {
                return this.documentDateFieldSpecified;
            }
            set
            {
                this.documentDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedAllowEducationDocument
    {

        private TErrorInfo errorInfoField;

        private string documentNumberField;

        private System.DateTime documentDateField;

        private bool documentDateFieldSpecified;

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string DocumentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DocumentDate
        {
            get
            {
                return this.documentDateField;
            }
            set
            {
                this.documentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentDateSpecified
        {
            get
            {
                return this.documentDateFieldSpecified;
            }
            set
            {
                this.documentDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedCustomDocument
    {

        private TErrorInfo errorInfoField;

        private string documentNumberField;

        private System.DateTime documentDateField;

        private bool documentDateFieldSpecified;

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string DocumentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DocumentDate
        {
            get
            {
                return this.documentDateField;
            }
            set
            {
                this.documentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocumentDateSpecified
        {
            get
            {
                return this.documentDateFieldSpecified;
            }
            set
            {
                this.documentDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedEntranceTestResult
    {

        private TErrorInfo errorInfoField;

        private string resultSourceTypeField;

        private string subjectNameField;

        private decimal resultValueField;

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string ResultSourceType
        {
            get
            {
                return this.resultSourceTypeField;
            }
            set
            {
                this.resultSourceTypeField = value;
            }
        }

        /// <remarks/>
        public string SubjectName
        {
            get
            {
                return this.subjectNameField;
            }
            set
            {
                this.subjectNameField = value;
            }
        }

        /// <remarks/>
        public decimal ResultValue
        {
            get
            {
                return this.resultValueField;
            }
            set
            {
                this.resultValueField = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageLogFailedApplicationCommonBenefit
    {

        private TErrorInfo errorInfoField;

        private string benefitKindNameField;

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public TErrorInfo ErrorInfo
        {
            get
            {
                return this.errorInfoField;
            }
            set
            {
                this.errorInfoField = value;
            }
        }

        /// <remarks/>
        public string BenefitKindName
        {
            get
            {
                return this.benefitKindNameField;
            }
            set
            {
                this.benefitKindNameField = value;
            }
        }

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageConflicts
    {

        private ImportResultPackageConflictsApplication[] applicationsField;

        private uint[] entranceTestResultsField;

        private ImportResultPackageConflictsApplicationCommonBenefits applicationCommonBenefitsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Application", IsNullable = false)]
        public ImportResultPackageConflictsApplication[] Applications
        {
            get
            {
                return this.applicationsField;
            }
            set
            {
                this.applicationsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EntranceTestsResultID", IsNullable = false)]
        public uint[] EntranceTestResults
        {
            get
            {
                return this.entranceTestResultsField;
            }
            set
            {
                this.entranceTestResultsField = value;
            }
        }

        /// <remarks/>
        public ImportResultPackageConflictsApplicationCommonBenefits ApplicationCommonBenefits
        {
            get
            {
                return this.applicationCommonBenefitsField;
            }
            set
            {
                this.applicationCommonBenefitsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageConflictsApplication
    {

        private string applicationNumberField;

        private System.DateTime registrationDateField;

        /// <remarks/>
        public string ApplicationNumber
        {
            get
            {
                return this.applicationNumberField;
            }
            set
            {
                this.applicationNumberField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegistrationDate
        {
            get
            {
                return this.registrationDateField;
            }
            set
            {
                this.registrationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ImportResultPackageConflictsApplicationCommonBenefits
    {

        private uint applicationCommonBenefitIDField;

        /// <remarks/>
        public uint ApplicationCommonBenefitID
        {
            get
            {
                return this.applicationCommonBenefitIDField;
            }
            set
            {
                this.applicationCommonBenefitIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Error", Namespace = "", IsNullable = false)]
    public partial class TError
    {

        private string errorCodeField;

        private string errorTextField;

        /// <remarks/>
        public string ErrorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }

        /// <remarks/>
        public string ErrorText
        {
            get
            {
                return this.errorTextField;
            }
            set
            {
                this.errorTextField = value;
            }
        }
    }
