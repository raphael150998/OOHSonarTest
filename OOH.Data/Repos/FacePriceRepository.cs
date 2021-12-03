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
    public class FacePriceRepository : OOHContext
    {
        public FacePriceRepository(IWebUserHelper userHelper) : base(userHelper)
        {
        }

        public async Task<IEnumerable<>> GetPrice()
        {

        }

        public async Task<bool> RemovePrice(long id)
        {

        }

        public async Task<ResultClass> AddOrUpdate(CarasPrecios collection)
        {

        }
    }
}
