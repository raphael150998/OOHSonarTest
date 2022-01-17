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
    public class CategoryRepo : OOHContext
    {
        private readonly ILogHelper _log;
        public CategoryRepo(IWebUserHelper userHelper, ILogHelper log) : base(userHelper)
        {
            _log = log;
        }     

        public async Task<IEnumerable<ClientesCategorias>> SelectClientCategory(string _Where = "")
        {
            return await SelectData<ClientesCategorias>("Select * from [dbo].[ClientesCategorias] "+ _Where);
        }

        public async Task<IEnumerable<CarasCategorias>> SelectFaceCategory(string _Where = "")
        {
            return await SelectData<CarasCategorias>("Select * from [dbo].[CarasCategorias]"+_Where,false);
        }
    }
}
