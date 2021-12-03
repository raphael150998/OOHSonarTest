using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Models.Site
{
    /// <summary>
    /// view model de sitio
    /// </summary>
    public class SiteVm
    {
        /// <summary>
        /// Id del sitio
        /// </summary>
        public Int64 SitioId { get; set; }

        /// <summary>
        /// Codigo del sitio
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Id del proveedor del sitio
        /// </summary>
        public Int64 ProveedorId { get; set; }

        /// <summary>
        /// Direccion 
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Referencias
        /// </summary>
        public string Referencia { get; set; }

        /// <summary>
        /// Latitud de ubicacion del sitio
        /// </summary>
        public double Latitud { get; set; }

        /// <summary>
        /// Longitud de ubicacion del sitio
        /// </summary>
        public double Longitud { get; set; }

        /// <summary>
        /// Id del municipio del sitio
        /// </summary>
        public int MunicipioId { get; set; }

        /// <summary>
        /// id de la zona del sitio
        /// </summary>
        public int ZonaId { get; set; }

        /// <summary>
        /// Determina si requiere permiso el ingreso
        /// </summary>
        public bool RequierePermiso { get; set; }

        /// <summary>
        /// Determina si esta activo
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Registro catastral
        /// </summary>
        public string RegistroCatastral { get; set; }

        /// <summary>
        /// Altura
        /// </summary>
        public double Altura { get; set; }

        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Id de la categoria del sitio
        /// </summary>
        public int CategoriaSitio { get; set; }

        /// <summary>
        /// id del tipo de estructura
        /// </summary>
        public int EstructuraTipo { get; set; }

        /// <summary>
        /// Determina si el sitio le pertenece a otra empresa
        /// </summary>
        public bool Ajeno { get; set; }

        /// <summary>
        /// Numero de dias con que se debe hacer la solicutud de permiso con antelacion
        /// </summary>
        public int DiasSolicitudPermiso { get; set; }

        /// <summary>
        /// Determina si tiene o no antena el sitio
        /// </summary>
        public bool TieneAntena { get; set; }

        /// <summary>
        /// Determina si aplica para instlacionm de antenas
        /// </summary>
        public bool AplicaAntena { get; set; }

        /// <summary>
        /// Determina si es privado
        /// </summary>
        public bool Privado { get; set; }

        /// <summary>
        /// Fecha de activacion del sitio
        /// </summary>
        public string FechaActivacion { get; set; }

        /// <summary>
        /// Notas a los insladores
        /// </summary>
        public string NotasInstaladores { get; set; }

        /// <summary>
        /// Observaciones web
        /// </summary>
        public string ObservacionesWeb { get; set; }

        /// <summary>
        /// Enlace web
        /// </summary>
        public string EnlaceWeb { get; set; }

        /// <summary>
        /// Id del proveedor de electricidad
        /// </summary>
        public long ProveedorElectricidadId { get; set; }

        /// <summary>
        /// Porcentaje
        /// </summary>
        public decimal Porcentaje { get; set; }

        /// <summary>
        /// numero de contador electrico
        /// </summary>
        public string ContadorElectrico { get; set; }

        /// <summary>
        /// NIC
        /// </summary>
        public string NIC { get; set; }
    }
}
