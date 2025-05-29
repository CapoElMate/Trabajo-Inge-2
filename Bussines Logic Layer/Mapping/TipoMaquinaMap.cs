using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Resolvers;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping
{
    public class TipoMaquinaMap : Profile
    {
        public TipoMaquinaMap()
        {
            CreateMap<TipoMaquina, TipoMaquinaDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));
            CreateMap<TipoMaquinaDto, TipoMaquina>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));
        }
    }
}
