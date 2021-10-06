using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Models.Site
{
    /// <summary>
    /// View Model de sitios
    /// </summary>
    public class SiteVm
    {
        public Int64 SitioId { get; set; }
        
        public string Codigo { get; set; }
        
        public Int64 ProveedorId { get; set; }

        public string NombreProveedor { get; set; }

        public string Direccion { get; set; }
        
        public string Referencia { get; set; }
        
        public double Latitud { get; set; }
        
        public double Longitud { get; set; }
        
        public int MunicipioId { get; set; }

        public string NombreMunicipio { get; set; }

        public int ZonaId { get; set; }

        public string NombreZona { get; set; }

        public bool RequierePermiso { get; set; }
        
        public bool Activo { get; set; }
        
        public string RegistroCatastral { get; set; }
        
        public double Altura { get; set; }
        
        public string Observaciones { get; set; }
    }
}
