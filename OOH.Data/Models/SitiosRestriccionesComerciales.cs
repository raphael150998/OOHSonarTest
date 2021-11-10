using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de tabla relacion entre sitios y restricciones comerciales
    /// </summary>
    public class SitiosRestriccionesComerciales
    {
        /// <summary>
        /// Id de tabla relacional sitios y restricciones comerciales
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id de sitio
        /// </summary>
        public long SitioId { get; set; }

        /// <summary>
        /// Id de restrccion comercial
        /// </summary>
        public int RestComercialId { get; set; }

        /// <summary>
        /// Comentarios
        /// </summary>
        public string Comentarios { get; set; }
    }
}
