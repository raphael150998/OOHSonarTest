using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Cotizacion
{
    public class QuotationDetailDto: CotizacionesDetalle
    {
        public string codigo { get; set; }
        public string referencia { get; set; }
        public string direccion { get; set; }
        public bool iluminada { get; set; }
        public string departamento { get; set; }
    }
}
