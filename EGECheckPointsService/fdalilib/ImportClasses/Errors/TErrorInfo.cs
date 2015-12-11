namespace Fdalilib.ImportClasses.Errors
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TErrorInfo
    {

        private uint errorCodeField;

        private string messageField;

        private ConflictItemsInfo conflictItemsInfoField;

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
        public ConflictItemsInfo ConflictItemsInfo
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
}

