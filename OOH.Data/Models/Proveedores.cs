using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de la base de proveedor
    /// </summary>
    public class Proveedores
    {
        public Proveedores()
        {
            Activo = true;
        }

        public Int64 ProveedorId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string NRC { get; set; }
        public string NIT { get; set; }
        public string Giro { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public bool PersonaJuridica { get; set; }
        public bool Activo { get; set; }
        public int CategoriaId { get; set; }
        public string RazonSocial { get; set; }
        public int MunicipioId { get; set; }


    }
}
