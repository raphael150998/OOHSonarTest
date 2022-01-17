using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de tabla antenas
    /// </summary>
    public class Antenas
    {
        public Antenas()
        {
            FechaCreacion = DateTime.Now;
        }

        /// <summary>
        /// Id de antena
        /// </summary>
        public long AntenaId { get; set; }

        /// <summary>
        /// Codigo visual de antena
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Id del operador
        /// </summary>
        public int OperadorId { get; set; }

        /// <summary>
        /// Id del tipo de estructura
        /// </summary>

        public int TipoEstructuraId { get; set; }

        /// <summary>
        /// Determina si el equipos esta o no en piso
        /// </summary>
        public bool EquipoEnPiso { get; set; }

        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; private set; }

        /// <summary>
        /// Fecha de instalacion de antena
        /// </summary>
        public DateTime FechaInstalacion { get; set; }

    }
}
