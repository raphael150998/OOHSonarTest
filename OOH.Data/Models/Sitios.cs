using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class Sitios
    {
        public Sitios()
        {
            Activo = true;
        }

        public Int64 SitioId { get; set; }
        public string Codigo { get; set; }
        public Int64 ProveedorId { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int MunicipioId { get; set; }
        public int ZonaId { get; set; }
        public bool RequierePermiso { get; set; }
        public bool Activo { get; set; }
        public string RegistroCatastral { get; set; }
        public double Altura { get; set; }
        public string Observaciones { get; set; }
    }
}
