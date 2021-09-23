using Moq;
using OOH.Data;
using OOH.Data.Interfaces;
using OOH.Test.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Test
{
    public class TestHelpers
    {
        /// <summary>
        /// Instancia un mock de el helper webUserHelper
        /// </summary>
        /// <param name="userConnection">Cadena de conexion simulada a la base </param>
        /// <param name="userId">Id simulado del usuario</param>
        /// <param name="platformId">Plataforma simulada</param>
        /// <param name="version">Version simulada</param>
        /// <returns></returns>
        public static IWebUserHelper GetWebUserHelper(WebUserHelperTestInputDto input)
        {
            Mock<IWebUserHelper> helper = new Mock<IWebUserHelper>();

            helper.Setup(x => x.GetUserConnectionString()).Returns(input.UserConnection);

            helper.Setup(x => x.GetUserId()).Returns(input.UserId);

            helper.Setup(x => x.GetUserPlatform()).Returns(input.PlatformId);

            helper.Setup(x => x.GetVersion()).Returns(input.Version);


            return helper.Object;
        }
    }
}
