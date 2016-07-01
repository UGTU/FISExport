using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbitExportProject.DataDecoders
{
    public static class DateTimeDecoder
    {
        public static string DateToString(DateTime? date)
        {
            return date?.ToString("yyyy-MM-dd") ?? string.Empty
                ;
        }
    }
}
