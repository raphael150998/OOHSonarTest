using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Models
{
    public class SitiosAccessTime
    {
        public SitiosAccessTime()
        {
            Active = true;
        }
        public long TimeId { get; set; }

        public long SitioId { get; set; }

        public DayOfWeek WeekDay { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool Active { get; set; }
    }
}
