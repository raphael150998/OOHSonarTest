using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Models.AccessTimeSite
{
    public class TimeVm
    {
        public TimeVm()
        {
            periods = new List<Period>();
        }

        public DayOfWeek day { get; set; }
        public List<Period> periods { get; set; }
    }

    public class Period
    {
        public string Start { get; set; }
        public string End { get; set; }
    }

}
