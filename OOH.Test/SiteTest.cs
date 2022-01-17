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
using OOH.Data.Helpers;
using OOH.Data.Dtos.Site;
using OOH.Data.Dtos.Logs;

namespace OOH.Test
{

    public class SiteTest
    {
        [Test]
        public async Task GettingSites_Ok()
        {
            //Objeto de configuracion para el webUserHelper
            WebUserHelperTestInputDto data = new()
            {
                UserConnection = "data source=192.168.10.238;initial catalog=OOH_VIVA;user id=jose;password=JR.2021;MultipleActiveResultSets=True;App=EntityFramework",
                UserId = 4,
                PlatformId = Platform.Web,
                Version = "TestProject 0.1"
            };

            List<LogOutputDto> logs = new()
            {
                new LogOutputDto()
                {
                    Login = "rafael.mendoza",
                    ActionDate = DateTimeOffset.Now,
                    Description = "Descripcion de prueba",
                    NameUser = "Rafael Mendoza",
                    Platform = Platform.Web.GetValueString(),
                    Version = "TestProject fake 0.1"
                }
            };

            Log log = new()
            {
                Id = 1,
                PlataformaId = Platform.Web,
                Version = "TestProject 0.1",
                Descripcion = "Descripcion",
                Entidad = nameof(Sitios),
                EntidadId = 1,
                Fecha = DateTimeOffset.Now,
                UserId = 1

            };

            SiteRepo _repo = new SiteRepo(TestHelpers.GetWebUserHelper(data), TestHelpers.GetLogHelper(logs, log));

            IEnumerable<SiteListDto> result = await _repo.GetList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public async Task GetSitesAsSelect2_Ok()
        {
            //Objeto de configuracion para el webUserHelper
            WebUserHelperTestInputDto data = new()
            {
                UserConnection = "data source=192.168.10.238;initial catalog=OOH_VIVA;user id=jose;password=JR.2021;MultipleActiveResultSets=True;App=EntityFramework",
                UserId = 4,
                PlatformId = Platform.Web,
                Version = "TestProject 0.1"
            };

            List<LogOutputDto> logs = new()
            {
                new LogOutputDto()
                {
                    Login = "rafael.mendoza",
                    ActionDate = DateTimeOffset.Now,
                    Description = "Descripcion de prueba",
                    NameUser = "Rafael Mendoza",
                    Platform = Platform.Web.GetValueString(),
                    Version = "TestProject fake 0.1"
                }
            };

            Log log = new()
            {
                Id = 1,
                PlataformaId = Platform.Web,
                Version = "TestProject 0.1",
                Descripcion = "Descripcion",
                Entidad = nameof(Sitios),
                EntidadId = 1,
                Fecha = DateTimeOffset.Now,
                UserId = 1

            };

            SiteRepo _repo = new SiteRepo(TestHelpers.GetWebUserHelper(data), TestHelpers.GetLogHelper(logs, log));

            Select2PagingOutputDto result = await _repo.GetListForSelect2(new Select2PagingInputDto() {  });

            Assert.IsNotNull(result.Results);
            Assert.IsTrue(result.Results.Count() > 0);
        }
    }
}
