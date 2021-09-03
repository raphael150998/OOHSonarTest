using OOH.Data.Helpers;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Interfaces
{
    interface IActionRepository<T> where T : class 
    {
        Task<T> Find(int Id);
        Task<IEnumerable<T>> Select(string _Where = "");
        Task<ResultClass> AddOrUpdate(T collection);
        Task<bool> Remove(int id);


    }
}
