using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Bussines_Logic_Layer.Resolvers.Usuarios;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping.Usuarios
{
    public class EmpleadoMap : Profile
    {
        public EmpleadoMap()
        {
            CreateMap<EmpleadoDTO, EmpleadoDto>()
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.Cliente.UsuarioRegistrado.DNI))
                .ForMember(dest => dest.nroEmpleado, opt => opt.MapFrom(src => src.nroEmpleado))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom<ClienteDtoToEmpeladoResolver>());

            CreateMap<EmpleadoDTO, EmpleadoDto>()
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.Cliente.UsuarioRegistrado.DNI))
                .ForMember(dest => dest.nroEmpleado, opt => opt.MapFrom(src => src.nroEmpleado))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom<ClienteDtoToEmpeladoResolver>());
        }
    }
}
