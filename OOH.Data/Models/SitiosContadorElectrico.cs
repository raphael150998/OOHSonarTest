using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class SitiosContadorElectrico
    {
        public long Id { get; set; }
        public long SitioId { get; set; }

        public long ProveedorId { get; set; }
        public int ContadorId { get; set; }
        public bool Active { get; set; }
        public decimal Porcentaje { get; set; }

        public string ContadorElectrico { get; set; }

        public string NIC { get; set; }
    }
}
