using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    public class AccessTime
    {
        public long TimeId { get; set; }

        public long SitioId { get; set; }

        public DayOfWeek WeekDay { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool Active { get; set; }
    }
}
