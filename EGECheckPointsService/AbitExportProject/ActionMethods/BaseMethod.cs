using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbitExportProject.ActionMethods
{
    public interface IBaseMethod
    {
        int Year { get; }
        string ToString();
        void Run(Func<string,string> askMore);
    }
}
