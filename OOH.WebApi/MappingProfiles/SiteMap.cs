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
            CreateMap<Sitios, SiteVm>()
                .ForMember(x => x.FechaActivacion, o => o.MapFrom(y => y.FechaActivacion.HasValue ? y.FechaActivacion.Value.ToString() : ""));

            CreateMap<SiteVm, Sitios>().
                ForMember(x => x.FechaActivacion, o => o.MapFrom(y => string.IsNullOrEmpty(y.FechaActivacion) ? new Nullable<DateTime>() : DateTime.Parse(y.FechaActivacion)));
        }
    }
}
