using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbitExportProject
{
  
    internal struct ExportParam
    {
        public ExportParam(ExportType exporttParamType, decimal abitId)
        {
            ExportType = exporttParamType;
            AbitId = abitId;
        }

        public ExportType ExportType { get; }

        public decimal AbitId { get; }
    }

    /// <summary>
    ///     Определяет тип экспорта
    /// </summary>
    internal enum ExportType
    {
        /// <summary>
        ///     Экспортируется заданный абитуриент
        /// </summary>
        Single,

        /// <summary>
        ///     Экспортируется пакет заявлений на основе системного журнала
        /// </summary>
        Batch,
        /// <summary>
        /// Необходимые параметры не переданы
        /// </summary>
        Undefifined
    }
}
