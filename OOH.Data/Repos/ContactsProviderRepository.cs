using Dapper;
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
    public class ContactsProviderRepository : OOHContext, IBaseRepository<ProveedoresContactos>
    {
        private readonly ILogHelper _log;
        public ContactsProviderRepository(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }

        public async Task<ResultClass> AddOrUpdate(ProveedoresContactos collection)
        {
            ResultClass result = new ResultClass();

            DynamicParameters param = new DynamicParameters(collection);

            if (collection.id == 0)
            {
                result = new ResultClass { data =await PostData(@"Insert into [dbo].[ProveedoresContactos] ([Direccion],[Ciudad],[Contacto],[Telefono],[Celular],[Email],[ProveedorId]) Values(@Direccion,@Ciudad,@Contacto,@Telefono,@Celular,@Email,@ProveedorId)", true, param, false) };
            }
            else
            {

                result = new ResultClass { data = await UpdateData(" Update [dbo].[ProveedoresContactos] set [Direccion] = @Direccion , Ciudad = @Ciudad, Contacto = @Contacto , Telefono = @Telefono ,Celular = @Celular, Email = @Email Where id = @id", true, param, false) };
            }

            await _log.AddLog(new LogDto()
            {
                Descripcion = collection.id == 0 ? "Creación" : "Actualización",
                Entidad = nameof(ProveedoresContactos),
                EntidadId = collection.id == 0 ? (int)result.data : collection.id,
            });

            return result;
        }

        public async Task<ProveedoresContactos> Find(int Id)
        {
            return await FilterData<ProveedoresContactos>($"select * from ProveedoresContactos where id = {Id}");
        }

        public Task<IEnumerable<LogOutputDto>> GetLogs(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(int id)
        {
            await _log.AddLog(new LogDto()
            {
                Descripcion = "Eliminación",
                Entidad = nameof(ClientesContactos),
                EntidadId = id
            });

            return RemoveData($"delete from [dbo].[ProveedoresContactos] Where id = {id}", false).Result == 1 ? true : false;
        }

        public async Task<IEnumerable<ProveedoresContactos>> Select()
        {
            return await SelectData<ProveedoresContactos>($"select * from ProveedoresContactos");

        }
        public async Task<IEnumerable<ProveedoresContactos>> SelectByProvider(long provider)
        {
            return await SelectData<ProveedoresContactos>($"select * from ProveedoresContactos where ProveedorId = {provider}");

        }
    }
}
