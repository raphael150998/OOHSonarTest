using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Site
{
    /// <summary>
    /// DTO de salida para la tabla relacional entre sitios y referencias comerciales
    /// </summary>
    public class ReferencesSiteOutputDto
    {
        /// <summary>
        /// Id de la tabla relacional siutio y referencias comerciales
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id de sitio
        /// </summary>
        public long SitioId { get; set; }

        /// <summary>
        /// Id de referencia comercial
        /// </summary>
        public int ReferenciaId { get; set; }

        /// <summary>
        /// Comentarios
        /// </summary>
        public string Comentarios { get; set; }

        /// <summary>
        /// Nombre de la referencia
        /// </summary>
        public string Nombre { get; set; }
    }
}
