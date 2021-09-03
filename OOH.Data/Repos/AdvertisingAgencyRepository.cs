using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class AdvertisingAgencyRepository : OOHContext, IAdvertisingAgencyRepository
    {
        public Task<int> Create(AgenciasPublicidad agencia)
        {
            throw new NotImplementedException();
        }

        public Task<AgenciasPublicidad> Find(int Id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AgenciasPublicidad>> Select(string _Where = "")
        {
            throw new NotImplementedException();
        }

        public Task Update(AgenciasPublicidad agencia)
        {
            throw new NotImplementedException();
        }
    }
}
