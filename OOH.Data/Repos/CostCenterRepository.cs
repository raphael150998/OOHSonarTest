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
    public class CostCenterRepository : OOHContext, IBaseRepository<CentroCostos>
    {
        public CostCenterRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public Task<ResultClass> AddOrUpdate(CentroCostos centro)
        {
            throw new NotImplementedException();
        }

        public Task<CentroCostos> Find(int Id)
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

        public Task<IEnumerable<CentroCostos>> Select(string _Where = "")
        {
            throw new NotImplementedException();
        }
    }
}
