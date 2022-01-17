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
    public class OperatorRepo : OOHContext, IBaseRepository<Operadores>
    {
        public OperatorRepo(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public Task<ResultClass> AddOrUpdate(Operadores model)
        {
            throw new NotImplementedException();
        }

        public Task<Operadores> Find(int id)
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

        public Task<IEnumerable<Operadores>> Select()
        {
            throw new NotImplementedException();
        }
    }
}
