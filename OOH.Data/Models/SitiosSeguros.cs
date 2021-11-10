using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de la tabla relacional entre sitio y seguros
    /// </summary>
    public class SitiosSeguros
    {
        /// <summary>
        /// id de la tabla relacional
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// id del sitio
        /// </summary>
        public long SitioId { get; set; }


        /// <summary>
        /// id del seguro
        /// </summary>
        public int SeguroId { get; set; }
    }
}
