using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    public class ProveedoresContactos
    {
        public Int64 id { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public Int64 ProveedorId { get; set; }
    }
}
