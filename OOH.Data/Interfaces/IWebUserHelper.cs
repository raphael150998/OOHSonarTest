using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    public interface IWebUserHelper
    {
        /// <summary>
        /// Obtiene la cadena de conexión hacia la base del usuario logueado
        /// </summary>
        /// <returns></returns>
        string GetUserConnectionString();
    }
}
