using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// modelo de tabla relacional entre sitio y proveedor
    /// </summary>
    public class SitiosProveedor
    {
        /// <summary>
        /// Id de tabla realacional
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id del sitio
        /// </summary>
        public long SitioId { get; set; }

        /// <summary>
        /// Id del proveedor
        /// </summary>
        public long ProveedorId { get; set; }

        /// <summary>
        /// porcentaje
        /// </summary>
        public decimal Porcentaje { get; set; }

        /// <summary>
        /// Monto
        /// </summary>
        public decimal Monto { get; set; }
    }
}
