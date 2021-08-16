using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Helpers
{
    public class ResultClass
    {
        public bool State { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }     
        public string Condition { get; set; }
        public Exception Exception { get; set; }

    }
}
