using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Site
{
    /// <summary>
    /// DTO de salida de la relacion entre tabla segurosTipo y Sitios
    /// </summary>
    public class InsuranceSiteOutputDto
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

        /// <summary>
        /// Nombre de seguro
        /// </summary>
        public string Nombre { get; set; }
    }
}
