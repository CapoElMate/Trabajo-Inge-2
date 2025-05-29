using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Bussines_Logic_Layer.Resolvers.Alquiler;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping
{
    public class AlquilerMap: Profile
    {
        public AlquilerMap() {
            CreateMap<Alquiler, AlquilerDto>()
                .ForMember(dest => dest.DNIEmpleadoEfectivizo, opt => opt.MapFrom(src => src.Empleado.DNI))
                .ForMember(dest => dest.InfoAsentada, opt => opt.MapFrom<InfoAsentadaToAlquilerDto>());
            CreateMap<AlquilerDto, Alquiler>()
                .ForMember(dest => dest.DNIEmpleado, opt => opt.MapFrom(src => src.DNIEmpleadoEfectivizo))
                .ForMember(dest => dest.InfoAsentada, opt => opt.MapFrom<InfoAsentadaDtoToAlquiler>());
            CreateMap<CreateAlquilerDto, Alquiler>()
                .ForMember(dest => dest.DNIEmpleado, opt => opt.MapFrom(src => src.DNIEmpleadoEfectivizo))
                .ForMember(dest => dest.InfoAsentada, opt => opt.MapFrom<CreateInfoAsentadaDtoToAlquiler>());
        }
    }
}
