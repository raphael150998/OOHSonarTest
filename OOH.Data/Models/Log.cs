using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Models
{
    /// <summary>
    /// Modelo de base de datos de la tabla Log
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Id del log (autoincrementable)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Usuario que ejecuto la accion
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Entidad con que se interactua (tabla de la base de datos)
        /// </summary>
        public string Entidad { get; set; }

        /// <summary>
        /// Descripcion de la accion realizada
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha en que se realizo la accion
        /// </summary>
        public DateTimeOffset Fecha { get; set; }

        /// <summary>
        /// Id de la entidad con la que se interactua
        /// </summary>
        public long EntidadId { get; set; }

        /// <summary>
        /// Plataforma desde la cual se realiza la accion
        /// </summary>
        public Platform PlataformaId { get; set; }

        /// <summary>
        /// Version del software
        /// </summary>
        public string Version { get; set; }
    }
}
