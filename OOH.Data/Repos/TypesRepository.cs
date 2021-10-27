using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOH.Data.Interfaces;
using OOH.Data.Models;

namespace OOH.Data.Repos
{
    public class TypesRepository:OOHContext
    {
        public TypesRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<IEnumerable<CarasTipos>> SelectCaraTipos(string _Where="")
        {
            return await SelectData<CarasTipos>("select * from [dbo].[CarasTipos] "+ _Where);
        }

    }
}
