using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointProvider
{
    public interface IPowerpointProvider
    {
        Task<byte[]> GetPowerpoint(string templatePath);
    }
}
