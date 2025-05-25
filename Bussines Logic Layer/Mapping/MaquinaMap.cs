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
    public class MaquinaMap : Profile
    {
        public MaquinaMap()
        {
            CreateMap<Maquina, MaquinaDto>()
                .ForMember(dest => dest.IdMaquina, opt => opt.MapFrom(src => src.idMaquina))
                .ForMember(dest => dest.PermisosEspeciales, opt => opt.MapFrom<ListPEToMaquinaDto>())
                .ForMember(dest => dest.TagsMaquina, opt => opt.MapFrom<ListTAToMaquinaDto>())
                .ForMember(dest => dest.TipoMaquina, opt => opt.MapFrom<TMToMaquinaDto>());

            CreateMap<CreateMaquinaDto, Maquina>()
                .ForMember(dest => dest.PermisosEspeciales, opt => opt.MapFrom<ListPEToMaquina>())
                .ForMember(dest => dest.TagsMaquina, opt => opt.MapFrom<ListTAToMaquina>())
                .ForMember(dest => dest.TipoMaquina, opt => opt.MapFrom<TMToMaquina>())
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom<ModeloMToMaquinaResolver>())
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoMaquina.Tipo))
                .ForMember(dest => dest.ModeloName, opt => opt.MapFrom(src => src.Modelo.Modelo));
        }
    }
}
