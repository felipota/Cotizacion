#region Usings
using System.Collections.Generic;
#endregion

namespace Cotizacion.Models
{
    /// <summary>
    /// Find By Data
    /// </summary>
    public class FindByData
    {
        /// <summary>
        /// Data string or numeric values
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// List Data string or numeric values
        /// </summary>
        public List<string> ListData { get; set; }
    }
}
