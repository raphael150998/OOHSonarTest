using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Helpers
{
    public class ResultClass
    {
        public ResultClass()
        {
            //Por defecto el resultado es positivo y se cambia el valor en caso de ser negativo
            state = true;
        }

        public bool state { get; set; } = true;
        public string message { get; set; }
        public Object data { get; set; }     
        public string condition { get; set; }
        public Exception exception { get; set; }

    }
}
