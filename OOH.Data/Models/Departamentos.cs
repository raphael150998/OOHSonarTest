using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de la tabla departamentos
    /// </summary>
    public class Departamentos
    {
        /// <summary>
        /// Id del departamento
        /// </summary>
        public int DepartamentoId { get; set; }

        /// <summary>
        /// Nombre del departamento
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Zona
        /// </summary>
        public string Zona { get; set; }

    }
}
