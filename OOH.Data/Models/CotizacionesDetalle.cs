using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    public class CotizacionesDetalle
    {
        public Int64 Id { get; set; }
        public Int64 CotizacionId { get; set; }
        public Int64 CaraId { get; set; }
        public Decimal CostoArrendamiento { get; set; }
        public Decimal CostoImpresion { get; set; }
        public Decimal CostoInstalacion { get; set; }
        public Decimal CostoSaliente { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    
    }
}
