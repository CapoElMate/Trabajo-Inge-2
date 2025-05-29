using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.InfoAsentada;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping
{
    public class InfoAsentadaMap: Profile
    {
        public InfoAsentadaMap() {
            CreateMap<Domain_Layer.Entidades.InfoAsentada, InfoAsentadaDto>()
                .ForMember(dest => dest.idAlquiler, opt => opt.MapFrom(src => src.idAlquiler))
                .ForMember(dest => dest.DNIEmpleadoAsento, opt => opt.MapFrom(src => src.DNIEmpleado))
                .ForMember(dest => dest.fec, opt => opt.MapFrom(src => src.fec))
                .ForMember(dest => dest.Contenido, opt => opt.MapFrom(src => src.Contenido))
                .ForMember(dest => dest.idInfo, opt => opt.MapFrom(src => src.idInfo));

            CreateMap<InfoAsentadaDto, InfoAsentada>()
                .ForMember(dest => dest.idAlquiler, opt => opt.MapFrom(src => src.idAlquiler))
                .ForMember(dest => dest.DNIEmpleado, opt => opt.MapFrom(src => src.DNIEmpleadoAsento))
                .ForMember(dest => dest.fec, opt => opt.MapFrom(src => src.fec))
                .ForMember(dest => dest.Contenido, opt => opt.MapFrom(src => src.Contenido))
                .ForMember(dest => dest.idInfo, opt => opt.MapFrom(src => src.idInfo));
        }
    }
}
