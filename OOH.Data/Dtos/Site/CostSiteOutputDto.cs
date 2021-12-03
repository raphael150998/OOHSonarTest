using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Site
{
    /// <summary>
    /// DTO de salida para costos y sitios
    /// </summary>
    public class CostSiteOutputDto
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
        public decimal? Porcentaje { get; set; }
        /// <summary>
        /// monto
        /// </summary>
        public decimal? Monto { get; set; }

        /// <summary>
        /// Nombre del costo
        /// </summary>
        public string Nombre { get; set; }
    }
}
