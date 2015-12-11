using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdalilib.XMLCODE
{
    public partial class Root
    {


        private PackageData packageDataField;


        /// <remarks/>
        public PackageData PackageData
        {
            get { return this.packageDataField; }
            set { this.packageDataField = value; }
        }
    }
}
