using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de la tabla operadores
    /// </summary>
    public class Operadores
    {
        /// <summary>
        /// Id del operador
        /// </summary>
        public int OperadorId { get; set; }

        /// <summary>
        /// Codigo visual del operador
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre del operador
        /// </summary>
        public string Nombre { get; set; }

    }
}
