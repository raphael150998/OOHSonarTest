using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de tipos de seguro
    /// </summary>
    public class SegurosTipos
    {
        /// <summary>
        /// id de seguro
        /// </summary>
        public int SeguroId { get; set; }

        /// <summary>
        /// Nombre del seguro
        /// </summary>
        public string Nombre { get; set; }
    }
}
