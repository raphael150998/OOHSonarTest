using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Caras
{
    public class FaceDto
    {
        public Int64 CaraId { get; set; }
        public Int64 SitioId { get; set; }
        public string sitio { get; set; }
        public string Codigo { get; set; } //
        public int TipoId { get; set; }
        public string tipo { get; set; }
        public string municipio { get; set; }
        public bool activa { get; set; }
    }
}
