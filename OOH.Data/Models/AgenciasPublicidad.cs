using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de la base de AgenciasPublicidad
    /// </summary>
    public class AgenciasPublicidad
    {
        /// <summary>
        /// Id de la agencia
        /// </summary>
        public int AgenciaId { get; set; }

        /// <summary>
        /// Nombre de la agencia
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Comision
        /// </summary>
        public float Comision { get; set; }

        /// <summary>
        /// Determina si la agencia esta activa
        /// </summary>
        public bool Activo { get; set; }
    }
}
