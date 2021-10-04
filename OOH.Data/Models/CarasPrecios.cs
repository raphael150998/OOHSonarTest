using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    public class CarasPrecios
    {
        public int Id { get; set; }
        public Int64 CaraId { get; set; }
        public int TipoId { get; set; }
        public decimal Precio { get; set; }
    }
}
