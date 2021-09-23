using NUnit.Framework;
using OOH.Data;
using OOH.Data.Dtos;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.Test.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Test
{
    public class LogHelperTest
    {
        [Test]
        public async Task GettingLogsFromClientesContactosRegister_Ok()
        {

            //Objeto de configuracion para el webUserHelper
            WebUserHelperTestInputDto data = new()
            {
                UserConnection = "data source=192.168.10.238;initial catalog=OOH_VIVA;user id=jose;password=JR.2021;MultipleActiveResultSets=True;App=EntityFramework",
                UserId = 4,
                PlatformId = Platform.Web,
                Version = "TestProject 0.1"
            };

            //Intancia del helper de webUserhelper
            LogHelper _repo = new LogHelper(TestHelpers.GetWebUserHelper(data));

            List<LogOutputDto> result = (await _repo.GetLogs(new LogInputDto(20, nameof(ClientesContactos)))).ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
