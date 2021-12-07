using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{

    /// <summary>
    /// Modelo de la tabla municipio
    /// </summary>
    public class Municipios
    {
        /// <summary>
        /// Id del municipio
        /// </summary>
        public int MunicipioId { get; set; }

        /// <summary>
        /// Id del departamento
        /// </summary>
        public int DepartamentoId { get; set; }

        /// <summary>
        /// Nombre del municipio
        /// </summary>
        public string Nombre { get; set; }
    }
}
