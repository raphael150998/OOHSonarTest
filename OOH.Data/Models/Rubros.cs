using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de la tabla Rubros
    /// </summary>
    public class Rubros
    {
        public Rubros()
        {
            Activo = true;
        }

        /// <summary>
        /// Identificador del rubro
        /// </summary>
        public int RubroId { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Determina si esta activo
        /// </summary>
        public bool Activo { get; set; }

    }
}
