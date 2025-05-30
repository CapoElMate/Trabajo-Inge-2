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
    public class PoliticaDeCancelacionMap:Profile
    {
        public PoliticaDeCancelacionMap()
        {
            CreateMap<PoliticaDeCancelacion, PoliticaDeCancelacionDto>()
                .ForMember(dest => dest.Politica, opt => opt.MapFrom(src => src.Politica))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

        }
    }
}
