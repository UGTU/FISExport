using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbitExportProject.Data
{
    public partial class Abit_Campaign
    {
        public DictionaryContent StatusPK 
        {
            get { return DictionaryContent; }
            set { DictionaryContent = value; }
        }

        public DictionaryContent TypePK
        {
            get { return DictionaryContent1; }
            set { DictionaryContent1 = value; }
        }
    }
}
