using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Site
{
    public class RestrictionSiteOutputDto
    {
        /// <summary>
        /// Id de la tabla relacional siutio y rewtriccion
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id de sitio
        /// </summary>
        public long SitioId { get; set; }

        /// <summary>
        /// Id de restriccion
        /// </summary>
        public int RestriccionId { get; set; }

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
