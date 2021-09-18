using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class ContadorElectrico
    {
        public int ContadorId { get; set; }
        public string NIC { get; set; }
        public bool Activo { get; set; }
        public Int64 ProveedorId { get; set; }
    }
}
