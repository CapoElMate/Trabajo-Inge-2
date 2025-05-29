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
    public class MarcaMap : Profile
    {
        public MarcaMap()
        {
            CreateMap<Marca, MarcaDto>()
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.MarcaName));
            CreateMap<MarcaDto, Marca>()
                .ForMember(dest => dest.MarcaName, opt => opt.MapFrom(src => src.Marca));
        }
    }
}
