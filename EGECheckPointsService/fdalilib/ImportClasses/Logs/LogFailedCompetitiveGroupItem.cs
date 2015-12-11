﻿using Fdalilib.ImportClasses.Errors;

namespace Fdalilib.ImportClasses.Logs
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class LogFailedCompetitiveGroupItem {
    
        private TErrorInfo errorInfoField;
    
        private string directionCodeField;
    
        private string directionNameField;
    
        private string competitiveGroupNameField;
    
        /// <remarks/>
        public TErrorInfo ErrorInfo {
            get {
                return this.errorInfoField;
            }
            set {
                this.errorInfoField = value;
            }
        }
    
        /// <remarks/>
        public string DirectionCode {
            get {
                return this.directionCodeField;
            }
            set {
                this.directionCodeField = value;
            }
        }
    
        /// <remarks/>
        public string DirectionName {
            get {
                return this.directionNameField;
            }
            set {
                this.directionNameField = value;
            }
        }
    
        /// <remarks/>
        public string CompetitiveGroupName {
            get {
                return this.competitiveGroupNameField;
            }
            set {
                this.competitiveGroupNameField = value;
            }
        }
    }
}