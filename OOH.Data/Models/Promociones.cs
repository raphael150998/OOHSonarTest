using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de la tabla promociones
    /// </summary>
    public class Promociones
    {
        public Promociones()
        {
            Activo = true;
        }
        /// <summary>
        /// Identificador
        /// </summary>
        public long PromocionId { get; set; }

        /// <summary>
        /// Descripcion de la promocion
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Determina si esta o no activo
        /// </summary>
        public bool Activo { get; set; }

    }
}
