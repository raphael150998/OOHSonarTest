using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Models.Site.Permission
{
    /// <summary>
    /// View model de permisosSitios 
    /// </summary>
    public class SitePermissionVm
    {
        public SitePermissionVm()
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
        public string FechaInicio { get; set; }

        /// <summary>
        /// Fecha fin
        /// </summary>
        public string FechaFin { get; set; }

        /// <summary>
        /// fecha de inicio de cuotas
        /// </summary>
        public string FechaInicioCuotas { get; set; }

        /// <summary>
        /// determina si esta activo 
        /// </summary>
        public bool Activo { get; set; }
    }
}
