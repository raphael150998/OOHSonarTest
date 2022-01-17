using Microsoft.AspNetCore.Mvc;
using OOH.Data.Models;
using OOH.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.ApiControllers
{
    public class PromotionApiController : ControllerBase
    {
        private readonly PromotionRepo _repo;

        public PromotionApiController(PromotionRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetListForDropdown()
        {
            List<Promociones> references = (await _repo.Select()).ToList();

            object result = references.Count() > 0 ? references.Select(x => new { Id = x.PromocionId, Name = x.Descripcion }) : new List<object>() { };

            return Ok(result);
        }
    }
}
