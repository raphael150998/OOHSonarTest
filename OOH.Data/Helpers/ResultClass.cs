using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Helpers
{
    public class ResultClass
    {
        public bool state { get; set; } = true;
        public string message { get; set; }
        public Object data { get; set; }     
        public string condition { get; set; }
        public Exception exception { get; set; }

    }
}
