using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    public class Materiales
    {
        public int MaterialId { get; set; }
        public string Codigo { get; set; }
        public string mateNombre { get; set; }
        public bool mateActivo { get; set; }
    }
}
