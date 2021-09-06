using AutoMapper;
using OOH.Data.Models;
using OOH.WebApi.Models.Agency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.MappingProfiles
{
    public class AgencyMap : Profile
    {
        public AgencyMap()
        {
            CreateMap<AgenciasPublicidad, AgencyVm>()
                .ForMember(x => x.Id, o => o.MapFrom(y => y.AgenciaId))
                .ForMember(x => x.Name, o => o.MapFrom(y => y.Nombre))
                .ForMember(x => x.Rate, o => o.MapFrom(y => y.Comision));
        }
    }
}
