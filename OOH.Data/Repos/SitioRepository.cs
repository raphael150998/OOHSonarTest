using Dapper;
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
    public class SitioRepository : OOHContext, IBaseRepository<Sitios>
    {
        private readonly ILogHelper _log;
        public SitioRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(Sitios sitio)
        {
            ResultClass result = new ResultClass();

            string sql = sitio.SitioId == 0 ?
                            "INSERT INTO Sitios (ProveedorId, Direccion, Referencia, Latitud, Longitud, MunicipioId, ZonaId, RequierePermiso, Activo, RegistroCatastral, Altura, Observaciones) VALUES (@ProveedorId, @Direccion, @Referencia, @Latitud, @Longitud, @MunicipioId, @ZonaId, @RequierePermiso, @Activo, @RegistroCatastral, @Altura, @Observaciones)" :
                            "UPDATE Sitios SET ProveedorId = @ProveedorId, Direccion = @Direccion, Referencia = @Referencia, Latitud = @Latitud, Longitud = @Longitud, MunicipioId = @MunicipioId, ZonaId = @ZonaId, RequierePermiso = @RequierePermiso, Activo = @Activo, RegistroCatastral = @RegistroCatastral, Altura = @Altura, Observaciones = @Observaciones WHERE SitioId = @SitioId";

            result.data = sitio.SitioId == 0 ? await PostData(sql, true, new DynamicParameters(sitio)) : await UpdateData(sql, true, new DynamicParameters(sitio));

            result.state = (int)result.data > 0;

            await _log.AddLog(new LogDto()
            {
                Descripcion = sitio.SitioId == 0 ? "Creación" : "Actualización",
                Entidad = nameof(Sitios),
                EntidadId = sitio.SitioId == 0 ? (int)result.data : sitio.SitioId,
            });

            return result;
        }

        public async Task<Sitios> Find(int Id)
        {
            return await FilterData<Sitios>($"SELECT * FROM Sitios WHERE SitioId = {Id}");
        }

        public async Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            return await _log.GetLogs(new LogInputDto(id, nameof(Sitios)));
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(Sitios),
                EntidadId = id
            });

            return (await RemoveData($"DELETE FROM Sitios WHERE SitioId = {id}")) > 0;
        }

        public async Task<IEnumerable<Sitios>> Select(string _Where = "")
        {
            return await SelectData<Sitios>("SELECT * FROM Sitios " + _Where);
        }
    }
}
