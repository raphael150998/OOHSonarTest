using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    public class Zonas
    {
        public int ZonaId { get; set; }

        /// <summary>
        /// Nombre de la zona
        /// </summary>
        /// <remarks>EL maximo de caracteres es 100</remarks>
        public string Nombre { get; set; }
    }
}
