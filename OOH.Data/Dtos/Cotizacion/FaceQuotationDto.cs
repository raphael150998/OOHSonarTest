using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Cotizacion
{
    public class FaceQuotationDto: Caras
    {
        public string tipo { get; set; }
        public string categoria { get; set; }
        public string direccion { get; set; } //
        public  double precio { get; set; } //
        public bool iluminada { get; set; } //

    }
}
