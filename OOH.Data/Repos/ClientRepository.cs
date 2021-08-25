using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class ClientRepository : OOHContext, IClientRepository
    {
        public string ConectionnString { get; set; }
        public ClientRepository(string _stringcontection)
        {
            this.ConectionnString = _stringcontection;
        }
        public async Task<int> Create(Clientes client)
        {
            throw new NotImplementedException();
        }

        public async Task<Clientes> Find(int Id)
        {
            return FilterData<Clientes>($"Select * from [dbo].[clientes] Where ClienteId = {Id}",false,null,ConectionnString).Result;

        }

        public async Task Remove(int id)
        {
            await PostData($"Update [dbo].[clientes] set Activo = false where ClienteId = {id}");

        }

        public async Task<IEnumerable<Clientes>> Select(string _Where = "")
        {
           return SelectData<Clientes>("select * from [dbo].[clientes] "+_Where,false,null,ConectionnString).Result.ToList();
        }

        public async Task Update(Clientes client)
        {
            throw new NotImplementedException();
        }
    }
}
