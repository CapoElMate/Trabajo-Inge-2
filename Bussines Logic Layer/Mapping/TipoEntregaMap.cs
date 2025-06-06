using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping
{
    public class TipoEntregaMap: Profile
    {
        public TipoEntregaMap()
        {
            CreateMap<TipoEntrega, TipoEntregaDto>()
                .ForMember(dest => dest.Entrega, opt => opt.MapFrom(src => src.Entrega));
            CreateMap<TipoEntregaDto, TipoEntrega>()
                .ForMember(dest => dest.Entrega, opt => opt.MapFrom(src => src.Entrega));
        }
    }
}
