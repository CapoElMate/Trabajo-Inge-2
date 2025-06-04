using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Resolvers.Publicacion;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping
{
    public class UbicacionMap:Profile
    {
        public UbicacionMap()
        {
            CreateMap<Ubicacion, UbicacionDto>()
                .ForMember(dest => dest.UbicacionName, opt => opt.MapFrom(src => src.UbicacionName));
            CreateMap<UbicacionDto, Ubicacion>()
                .ForMember(dest => dest.UbicacionName, opt => opt.MapFrom(src => src.UbicacionName));
        }
    }
}
