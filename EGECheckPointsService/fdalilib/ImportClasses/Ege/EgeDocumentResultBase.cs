namespace Fdalilib.ImportClasses.Ege
{
    /// <summary>
    /// Суперкласс для Correct и Incorrect EgeResult
    /// </summary>
    public class EgeDocumentResultBase
    {
        private string subjectNameField;
        private decimal scoreField;

        /// <remarks/>
        public string SubjectName {
            get {
                return this.subjectNameField;
            }
            set {
                this.subjectNameField = value;
            }
        }

        /// <remarks/>
        public decimal Score {
            get {
                return this.scoreField;
            }
            set {
                this.scoreField = value;
            }
        }
    }
}