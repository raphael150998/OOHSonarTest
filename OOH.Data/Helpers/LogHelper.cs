using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Helpers
{
    public class LogHelper
    {
        public static void Error(Exception ex)
        {
            Log.Error($"ocurrio un error en");
        }
    }
}
