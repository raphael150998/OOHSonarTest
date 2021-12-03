using OOH.Data.Dtos;
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
    public class MunicipalityRepository : OOHContext, IBaseRepository<Municipios>
    {
        private readonly ILogHelper _log;

        public MunicipalityRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public Task<ResultClass> AddOrUpdate(Municipios collection)
        {
            throw new NotImplementedException();
        }

        public Task<Municipios> Find(int Id)
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

        public async Task<IEnumerable<Municipios>> Select()
        {
            return await SelectData<Municipios>("Select t0.MunicipioId ,t0.DepartamentoId ,t1.Nombre as Departamento,t0.Nombre from Municipios t0 inner join Departamentos t1 on t0.DepartamentoId = t1.DepartamentoId");
        }
    }
}
