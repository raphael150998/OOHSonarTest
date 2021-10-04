using OOH.Data.Dtos;
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
    public class QuotationRepository : IBaseRepository<Cotizaciones>
    {
        public Task<ResultClass> AddOrUpdate(Cotizaciones collection)
        {
            throw new NotImplementedException();
        }

        public Task<Cotizaciones> Find(int Id)
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

        public Task<IEnumerable<Cotizaciones>> Select(string _Where = "")
        {
            throw new NotImplementedException();
        }
    }
}
