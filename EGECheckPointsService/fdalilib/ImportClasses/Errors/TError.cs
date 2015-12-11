namespace Fdalilib.ImportClasses.Errors
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Error", Namespace="", IsNullable=false)]
    public partial class Error {
    
        private string _errorCodeField;
    
        private string _errorTextField;
    
        /// <remarks/>
        public string ErrorCode {
            get {
                return this._errorCodeField;
            }
            set {
                this._errorCodeField = value;
            }
        }
    
        /// <remarks/>
        public string ErrorText {
            get {
                return this._errorTextField;
            }
            set {
                this._errorTextField = value;
            }
        }
    }
}