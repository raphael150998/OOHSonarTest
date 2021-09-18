using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class SitiosContadorElectrico
    {
        public Int64 Id { get; set; }
        public Int64 SitioId { get; set; }
        public int ContadorId { get; set; }
        public bool Activo { get; set; }
        public bool Porcentaje { get; set; }

    }
}
