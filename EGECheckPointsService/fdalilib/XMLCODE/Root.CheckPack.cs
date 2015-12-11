using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fdalilib.XMLCODE.CheckResults;

namespace Fdalilib.XMLCODE
{
    public partial class Root
    {
        private GetResultCheckApplication GetResultCheckApplicationField;

        public GetResultCheckApplication GetResultCheckApplication
        {
            get { return this.GetResultCheckApplicationField; }
            set { this.GetResultCheckApplicationField = value; }
        }

    }
}
