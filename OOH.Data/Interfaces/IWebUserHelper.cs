using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    public interface IWebUserHelper
    {
        /// <summary>
        ///  Obtiene la cadena de conexión hacia la base del usuario logueado
        /// </summary>
        /// <param name="connectionString">Si este parametro no va vacio entonces se devuelve el parametro enviado</param>
        /// <returns></returns>
        string GetUserConnectionString();

        /// <summary>
        /// Obtiene el id del usuario logueado en caso de que exista
        /// </summary>
        /// <returns></returns>
        int GetUserId();

        /// <summary>
        /// Obtiene la plataforma en la que el usuario se encuentra logueado
        /// </summary>
        /// <returns></returns>
        Platform GetUserPlatform();

        /// <summary>
        /// Obtiene la version de la plataforma que se esta utlizando
        /// </summary>
        /// <returns></returns>
        string GetVersion();
    }
}
