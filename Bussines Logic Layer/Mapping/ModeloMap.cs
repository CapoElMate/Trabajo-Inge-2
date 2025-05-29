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
    public class ModeloMap : Profile
    {
        public ModeloMap()
        {
            CreateMap<Modelo, ModeloDto>()
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.ModeloName))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca));

            CreateMap<ModeloDto, Modelo>()
                .ForMember(dest => dest.ModeloName, opt => opt.MapFrom(src => src.Modelo))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom<ModeloDtoToModeloResolver>())
                .ForMember(dest => dest.MarcaName, opt => opt.MapFrom(src => src.Marca.Marca));
        }
    }
}
