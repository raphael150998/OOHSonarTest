using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// modelo de tabla relacional entre sitios y permisos municipales
    /// </summary>
    public class SitiosPermisosMunicipales
    {
        public SitiosPermisosMunicipales()
        {
            Activo = true;
        }

        /// <summary>
        /// Id de la tabla relacion entre sitio y permisos municipales
        /// </summary>
        public long Id { get; set; }


        /// <summary>
        /// Id de sitio
        /// </summary>
        public long SitioId { get; set; }

        /// <summary>
        /// Id de permiso municipal
        /// </summary>
        public int PermisoId { get; set; }

        /// <summary>
        /// Id de estado
        /// </summary>
        public int EstadoId { get; set; }

        /// <summary>
        /// Monto
        /// </summary>
        public decimal Monto { get; set; }

        /// <summary>
        /// Frecuencia de pago
        /// </summary>
        public float FrecuenciaPago { get; set; }

        /// <summary>
        /// Fecha de inicio
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha fin
        /// </summary>
        public DateTime? FechaFin { get; set; }

        /// <summary>
        /// fecha de inicio de cuotas
        /// </summary>
        public DateTime FechaInicioCuotas { get; set; }

        /// <summary>
        /// determina si esta activo 
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Determina si el registro ha sido eliminado de manera logica en la base de datos
        /// </summary>
        public bool Eliminado { get; set; }
    }
}
