using AutoMapper;
using OOH.Data.Models;
using OOH.WebApi.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.MappingProfiles
{
    public class SiteMap : Profile
    {
        public SiteMap()
        {
            CreateMap<Sitios, SiteVm>();
        }
    }
}
