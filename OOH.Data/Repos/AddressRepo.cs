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
    public class AddressRepo : OOHContext
    {
        public AddressRepo(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<IEnumerable<Municipios>> Select()
        {
            return await SelectData<Municipios>("Select t0.MunicipioId ,t0.DepartamentoId ,t1.Nombre as Departamento,t0.Nombre from Municipios t0 inner join Departamentos t1 on t0.DepartamentoId = t1.DepartamentoId");
        }

        public async Task<IEnumerable<Municipios>> SelectMunicipiosByDepartamentoId(int departamentoId)
        {
            return await SelectData<Municipios>($"SELECT * FROM Municipios WHERE DepartamentoId = {departamentoId}");
        }

        public async Task<IEnumerable<Departamentos>> SelectDepartamentos()
        {
            return await SelectData<Departamentos>($"SELECT * FROM Departamentos");
        }

        public async Task<Departamentos> GetDepartamentoByMunicipioId(int idMunicipio)
        {
            return await FilterData<Departamentos>($"SELECT d.* FROM Departamentos d join Municipios m on m.DepartamentoId = d.DepartamentoId WHERE M.MunicipioId = {idMunicipio}");
        }

        public async Task<Municipios> GetMunicipioById(int idMunicupio)
        {
            return await FilterData<Municipios>($"SELECT * FROM Municipios WHERE MunicipioId = {idMunicupio}");
        }
    }
}
