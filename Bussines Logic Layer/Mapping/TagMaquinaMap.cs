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
    public class TagMaquinaMap : Profile
    {
        public TagMaquinaMap()
        {
            CreateMap<TagMaquina, TagMaquinaDto>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));
            CreateMap<TagMaquinaDto, TagMaquina>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));
        }
    }
}
