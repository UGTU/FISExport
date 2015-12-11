using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdalilib.XMLCODE
{
    public partial class Root
    {

        private DataForDelete dataForDeleteField;

        /// <remarks/>
        public DataForDelete DataForDelete
        {
            get
            {
                return this.dataForDeleteField;
            }
            set
            {
                this.dataForDeleteField = value;
            }
        }
    }
}
