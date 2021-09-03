﻿using OOH.Data.Helpers;
using OOH.Data.Interfaces;
using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Repos
{
    public class CategoryRepository : OOHContext, IActionRepository<ClientesCategorias>
    {
        public string ConectionnString { get; set; }
        public CategoryRepository(string _stringcontection)
        {
            this.ConectionnString = _stringcontection;
        }
        public Task<ResultClass> AddOrUpdate(ClientesCategorias collection)
        {
            throw new NotImplementedException();
        }

        public Task<ClientesCategorias> Find(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientesCategorias>> Select(string _Where = "")
        {
            return SelectData<ClientesCategorias>("Select * from [dbo].[ClientesCategorias] "+ _Where,false,null,this.ConectionnString).Result.ToList();
        }
    }
}
