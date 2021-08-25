using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    interface IClientRepository
    {
        Task<Clientes> Find(int Id);
        Task<IEnumerable<Clientes>> Select(string _Where = "");
        Task Update(Clientes client);        
        Task<int> Create(Clientes client);
        Task Remove(int id);


    }
}
