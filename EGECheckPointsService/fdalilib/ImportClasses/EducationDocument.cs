using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Fdalilib.ImportClasses
{
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public class EducationDocument
    {
        
        private string documentSeriesField;
        private string documentNumberField;
        private DateTime documentDateField;
        protected bool originalReceivedField;
        private string documentOrganizationField;
        private System.DateTime? originalReceivedDateField;
        private bool gPAFieldSpecified;
        private float? gPAField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", IsNullable = true)]
        public System.DateTime? OriginalReceivedDate
        {
            get { return this.originalReceivedDateField; }
            set { this.originalReceivedDateField = value; }
        }

        public bool ShouldSerializeOriginalReceivedDate()
        {
            return OriginalReceivedDate.HasValue;
        }

        /// <remarks/>
        public string DocumentSeries
        {
            get { return this.documentSeriesField; }
            set { this.documentSeriesField = value; }
        }

        /// <remarks/>
        public string DocumentNumber
        {
            get { return this.documentNumberField; }
            set { this.documentNumberField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public DateTime DocumentDate
        {
            get { return this.documentDateField; }
            set { this.documentDateField = value; }
        }

        /// <remarks/>
        public bool OriginalReceived
        {
            get { return this.originalReceivedField; }
            set { this.originalReceivedField = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool GPASpecified
        {
            get
            {
                return this.GPASpecified;
            }
            set
            {
                this.gPAFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public float? GPA
        {
            get { return this.gPAField; }
            set { this.gPAField = value; }
        }

        /// <remarks/>
        public string DocumentOrganization
        {
            get
            {
                return this.documentOrganizationField;
            }
            set
            {
                this.documentOrganizationField = value;
            }
        }
    }
}
