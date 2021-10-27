using OOH.Data.Dtos.Logs;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class ZoneRepository : OOHContext, IBaseRepository<Zonas>
    {
        public ZoneRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public Task<ResultClass> AddOrUpdate(Zonas collection)
        {
            throw new NotImplementedException();
        }

        public Task<Zonas> Find(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Zonas>> Select(string _Where = "")
        {
            return await SelectData<Zonas>("SELECT * FROM Zonas " + _Where);
        }
    }
}
