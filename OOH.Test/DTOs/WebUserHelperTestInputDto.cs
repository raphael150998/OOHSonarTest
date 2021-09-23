using OOH.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Test.DTOs
{
    /// <summary>
    /// DTO de entrada para la creacion de la interfaz mock de IWebUserHelper
    /// </summary>
    public class WebUserHelperTestInputDto
    {
        /// <summary>
        /// Cadena de conexion de simulacion a la base
        /// </summary>
        public string UserConnection { get; set; }

        /// <summary>
        /// Id simulado del usuario logueado
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id de la plataforma a simular
        /// </summary>
        public Platform PlatformId { get; set; }

        /// <summary>
        /// Version de la version
        /// </summary>
        public string Version { get; set; }
    }
}
