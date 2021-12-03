using NUnit.Framework;
using OOH.Data;
using OOH.Data.Dtos.AccessTime;
using OOH.Data.Dtos.Logs;
using OOH.Data.Helpers;
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
    public class TimeTest
    {
        [Test]
        public async Task ModifySiteTimes_Ok()
        {
            //Objeto de configuracion para el webUserHelper
            WebUserHelperTestInputDto data = new()
            {
                UserConnection = "data source=192.168.10.238;initial catalog=OOH_VIVA;user id=rafa;password=Orangelemon1;MultipleActiveResultSets=True;App=EntityFramework",
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
                Entidad = nameof(SitiosAccessTime),
                EntidadId = 1,
                Fecha = DateTimeOffset.Now,
                UserId = 1

            };

            AccessTimeRepository _repo = new AccessTimeRepository(TestHelpers.GetWebUserHelper(data), TestHelpers.GetLogHelper(logs, log));

            List<DayOfWeek> dayList = EnumHelper.GetItems<DayOfWeek>();

            List<TimeInputDto> list = new();

            foreach (var day in dayList)
            {
                TimeInputDto time = new()
                {
                    DayOfWeek = day,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(19, 0, 0),
                };

                list.Add(time);
            }

            await _repo.ModifyTimes(list, 67);
        }
    }
}
