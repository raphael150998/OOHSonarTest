using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Logs
{
    /// <summary>
    /// Objeto de transferencia de datos de log para cuando se requiere pasar mensajes personalizados
    /// </summary>
    public class LogDto
    {
        /// <summary>
        /// descripcion personalizada del log
        /// </summary>
        public string Descripcion { get; set; }

        public Platform? PlatformId { get; set; }

        public string Entidad { get; set; }

        public long EntidadId { get; set; }

        public string Version { get; set; }

        public string OldVersionJson { get; set; }
    }
}
