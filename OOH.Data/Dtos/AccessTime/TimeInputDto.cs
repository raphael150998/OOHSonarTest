using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.AccessTime
{
    /// <summary>
    /// DTO de entrada para regsitrar horas de acceso a un sitio
    /// </summary>
    public class TimeInputDto
    {
        /// <summary>
        /// Hora de inicio de periodo
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Hora de finalizacion de periodo
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Dia de la semana del periodo
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; }
    }
}
