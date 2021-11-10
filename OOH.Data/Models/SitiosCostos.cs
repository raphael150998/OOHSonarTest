using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de tabla relacional entre sitio y costos
    /// <list type="table" >
    /// <item>los costos relacionados a un sitio deben ser todos solo en monto o solo en porcentaje</item>
    /// </list>
    /// </summary>
    public class SitiosCostos
    {
        /// <summary>
        /// Id de la tabla relacional
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id del sitio
        /// </summary>
        public long SitioId { get; set; }

        /// <summary>
        /// id del centro de costo
        /// </summary>
        public int CostoId { get; set; }

        /// <summary>
        /// porcentaje
        /// </summary>
        public decimal Porcentaje { get; set; }

        /// <summary>
        /// monto
        /// </summary>
        public decimal Monto { get; set; }
    }
}
