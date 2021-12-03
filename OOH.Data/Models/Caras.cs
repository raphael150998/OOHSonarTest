using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    public class Caras
    {
        public Int64 CaraId { get; set; }
        public Int64 SitioId { get; set; }
        public string Codigo { get; set; } //
        public int TipoId { get; set; }
        public int CategoriaId { get; set; }
        public decimal Alto { get; set; }
        public decimal Ancho { get; set; }
        public string Sentido { get; set; }
        public decimal AlturaAlPiso { get; set; }
        public string MetodoInstalacion { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public string ReferenciaComercial { get; set; } //
        public int NumSpotDigital { get; set; }
        public bool CaraIluminada { get; set; }
        public string NotaInstalacion { get; set; }

    }
}
