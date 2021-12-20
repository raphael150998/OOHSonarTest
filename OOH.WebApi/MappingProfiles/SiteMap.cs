using AutoMapper;
using OOH.Data.Models;
using OOH.WebApi.Models.Site;
using OOH.WebApi.Models.Site.Permission;
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

            CreateMap<SiteVm, Sitios>()
                .ForMember(x => x.FechaActivacion, o => o.MapFrom(y => string.IsNullOrEmpty(y.FechaActivacion) ? new Nullable<DateTime>() : DateTime.Parse(y.FechaActivacion)));

            CreateMap<SitePermissionVm, SitiosPermisosMunicipales>()
                .ForMember(x => x.FechaInicio, o => o.MapFrom(y => DateTime.Parse(y.FechaInicio)))
                .ForMember(x => x.FechaInicioCuotas, o => o.MapFrom(y => DateTime.Parse(y.FechaInicioCuotas)))
                .ForMember(x => x.FechaFin, o => o.MapFrom(y => string.IsNullOrEmpty(y.FechaFin) ? new Nullable<DateTime>() : DateTime.Parse(y.FechaFin)));

            CreateMap<SitiosPermisosMunicipales, SitePermissionVm>()
                .ForMember(x => x.FechaInicio, o => o.MapFrom(y => y.FechaInicio.ToString()))
                .ForMember(x => x.FechaInicioCuotas, o => o.MapFrom(y => y.FechaInicioCuotas.ToString()))
                .ForMember(x => x.FechaFin, o => o.MapFrom(y => y.FechaFin.HasValue ? y.FechaFin.Value.ToString() : ""));
        }
    }
}
