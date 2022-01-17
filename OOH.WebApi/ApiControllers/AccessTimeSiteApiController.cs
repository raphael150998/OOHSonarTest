using Microsoft.AspNetCore.Mvc;
using OOH.Data.Dtos.AccessTime;
using OOH.Data.Models;
using OOH.Data.Repos;
using OOH.WebApi.Models.AccessTimeSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    [Route("api/time")]
    [ApiController]
    public class AccessTimeSiteApiController : ControllerBase
    {
        private readonly AccessTimeRepo _repo;

        public AccessTimeSiteApiController(AccessTimeRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("times/{id}")]
        public async Task<IActionResult> GetTimes(long id)
        {
            List<SitiosAccessTime> timesDb = await _repo.GetTimesBySitioId(id);

            List<TimeVm> timesVm = new();

            foreach (var dayDb in timesDb.GroupBy(x => x.WeekDay))
            {
                TimeVm timeVm = new()
                {
                    day = dayDb.Key
                };

                foreach (var timeDb in dayDb)
                {
                    timeVm.periods.Add(new Period()
                    {
                        Start = timeDb.StartTime.ToString(@"hh\:mm"),
                        End = timeDb.EndTime.ToString(@"hh\:mm")
                    });
                }

                timesVm.Add(timeVm);
            }

            return Ok(timesVm);
        }

        [HttpPost("times/{id}")]
        public async Task<IActionResult> SetTimes(long id, [FromBody] List<TimeVm> model)
        {
            List<TimeInputDto> times = new();

            foreach (var timeModel in model)
            {
                foreach (var period in timeModel.periods)
                {
                    TimeInputDto time = new()
                    {
                        DayOfWeek = timeModel.day,
                        StartTime = TimeSpan.Parse(period.Start),
                        EndTime = TimeSpan.Parse(period.End)
                    };

                    times.Add(time);
                }
            }

            await _repo.ModifyTimes(times, id);

            return Ok(true);
        }
    }
}
