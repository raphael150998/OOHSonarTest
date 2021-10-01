using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos
{
    /// <summary>
    /// DTO de salida de registro de acciones
    /// </summary>
    public class LogOutputDto
    {
        /// <summary>
        /// Correo o usuario con que se inicia sesion el actor que creo el log
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Nombre del actor
        /// </summary>
        public string NameUser { get; set; }

        /// <summary>
        /// Descripcion de la accion
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Fecha y hora en que se registro la accion
        /// </summary>
        public DateTimeOffset ActionDate { get; set; }

        /// <summary>
        /// Plataforma desde la cual se acciono el evento
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Version del software al momento de ejecutar la accion registrada
        /// </summary>
        public string Version { get; set; }
    }
}
