using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Caras
{
    public class FacePriceDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int TipoId  { get; set; }
        public double Precio { get; set; }
    }
}
