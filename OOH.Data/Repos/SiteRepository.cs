using Dapper;
using Newtonsoft.Json;
using OOH.Data.Dtos.Logs;
using OOH.Data.Dtos.Site;
using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class SiteRepository : OOHContext, IBaseRepository<Sitios>
    {
        private readonly ILogHelper _log;
        public SiteRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(Sitios sitio)
        {
            ResultClass result = new ResultClass();

            string sql = sitio.SitioId == 0 ?
                            "INSERT INTO Sitios (Codigo, ProveedorId, Direccion, Referencia, Latitud, Longitud, MunicipioId, ZonaId, RequierePermiso, Activo, RegistroCatastral, Altura, Observaciones) VALUES (@Codigo, @ProveedorId, @Direccion, @Referencia, @Latitud, @Longitud, @MunicipioId, @ZonaId, @RequierePermiso, @Activo, @RegistroCatastral, @Altura, @Observaciones)" :
                            "UPDATE Sitios SET Codigo = @Codigo, ProveedorId = @ProveedorId, Direccion = @Direccion, Referencia = @Referencia, Latitud = @Latitud, Longitud = @Longitud, MunicipioId = @MunicipioId, ZonaId = @ZonaId, RequierePermiso = @RequierePermiso, Activo = @Activo, RegistroCatastral = @RegistroCatastral, Altura = @Altura, Observaciones = @Observaciones WHERE SitioId = @SitioId";

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

        /// <summary>
        ///  Listado de sitios
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SiteListDto>> GetList()
        {
            #region sql
            string sql = "select s.*, (select p.Nombre from Proveedores p where p.ProveedorId= s.ProveedorId) as NombreProveedor, (select m.Nombre from Municipios m where m.MunicipioId = s.MunicipioId) as NombreMunicipio, 'Ninguna' as NombreZona  from Sitios s";
            #endregion

            return await SelectData<SiteListDto>(sql);
        }

        public async Task<Select2PagingOutputDto> GetListForSelect2(Select2PagingInputDto request)
        {
            Select2PagingOutputDto modelReturn = new();

            #region sqlCount
            string sqlCount = $"SELECT COUNT(T0.Id) FROM ( SELECT ~[Activo] as Disabled ,T0.[SitioId]  as Id ,CONCAT(T0.[Codigo], ' - ' ,T0.[Referencia], ' - ' ,(SELECT T1.[Nombre] FROM [dbo].[Municipios] T1 WHERE T1.MunicipioId=T0.MunicipioId), ' - ' ,(SELECT T2.[Nombre] FROM [dbo].[Departamentos] T2 INNER JOIN [dbo].[Municipios] T1 ON T1.DepartamentoId=T2.DepartamentoId WHERE T1.MunicipioId=T0.MunicipioId), ' - ' ,(SELECT T1.[Nombre] FROM  [dbo].[Zonas] T1 WHERE T1.ZonaId=T0.ZonaId )) AS [Text] FROM [dbo].[Sitios] T0 ) T0";
            #endregion

            string whereClause = "";

            if (request.Search.Count() > 0)
                whereClause = $" WHERE {string.Join(" OR ", request.Search.Select(x => $"Text like '%{x.ToUpper()}%'"))}";

            sqlCount += whereClause;

            int total = await FilterData<int>(sqlCount);

            int itemsToSkip = request.ItemsPerPage * (request.CurrentPage - 1);

            #region sql
            string sql = $"SELECT T0.* FROM (SELECT ~[Activo] as Disabled,T0.[SitioId]  as Id,CONCAT(T0.[Codigo], ' - ',T0.[Referencia], ' - ',(SELECT T1.[Nombre] FROM [dbo].[Municipios] T1 WHERE T1.MunicipioId=T0.MunicipioId), ' - ',(SELECT T2.[Nombre] FROM [dbo].[Departamentos] T2 INNER JOIN [dbo].[Municipios] T1 ON T1.DepartamentoId=T2.DepartamentoId WHERE T1.MunicipioId=T0.MunicipioId), ' - ',(SELECT T1.[Nombre] FROM  [dbo].[Zonas] T1 WHERE T1.ZonaId=T0.ZonaId )) AS [Text] FROM [dbo].[Sitios] T0) T0 {whereClause} ORDER BY Id OFFSET({itemsToSkip}) ROWS FETCH NEXT({request.ItemsPerPage}) ROWS ONLY ";

            string sqlSelected = $"SELECT TOP(1) T0.* FROM (SELECT ~[Activo] as [Disabled],T0.[SitioId]  as Id,CONCAT(T0.[Codigo], ' - ',T0.[Referencia], ' - ',(SELECT T1.[Nombre] FROM [dbo].[Municipios] T1 WHERE T1.MunicipioId=T0.MunicipioId), ' - ',(SELECT T2.[Nombre] FROM [dbo].[Departamentos] T2 INNER JOIN [dbo].[Municipios] T1 ON T1.DepartamentoId=T2.DepartamentoId WHERE T1.MunicipioId=T0.MunicipioId), ' - ',(SELECT T1.[Nombre] FROM  [dbo].[Zonas] T1 WHERE T1.ZonaId=T0.ZonaId )) AS [Text] FROM [dbo].[Sitios] T0) T0 WHERE Id = {request.Id}";

            string sqlEdit = $"{sqlSelected} UNION ALL SELECT * FROM ({sql} T1)";
            #endregion

            modelReturn.Results = (await SelectData<Select2OutputDto>(request.Id == 0 ? sql : sqlEdit)).ToList();

            //if (!string.IsNullOrEmpty(request.Id))
            //{
            //    #region sqlSelected              

            //    #endregion

            //    Select2OutputDto selectedOption = await FilterData<Select2OutputDto>(sqlSelected);

            //    selectedOption.Selected = true;

            //    modelReturn.Results.Add(selectedOption);
            //    modelReturn.Results = modelReturn.Results.OrderBy(x => x.Id).ToList();
            //}

            modelReturn.Pagination.More = (request.CurrentPage * request.ItemsPerPage) < total;

            return modelReturn;
        }
    }
}
