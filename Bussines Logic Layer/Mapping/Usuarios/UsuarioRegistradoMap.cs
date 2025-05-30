using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Bussines_Logic_Layer.Resolvers.Usuarios;

namespace Bussines_Logic_Layer.Mapping.Usuarios
{
    public class UsuarioRegistradoMap : Profile
    {
        public UsuarioRegistradoMap()
        {
            CreateMap<UsuarioRegistrado, UsuarioRegistradoDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.DNI))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.Edad))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Calle, opt => opt.MapFrom(src => src.Calle))
                .ForMember(dest => dest.Altura, opt => opt.MapFrom(src => src.Altura))
                .ForMember(dest => dest.Dpto, opt => opt.MapFrom(src => src.Dpto))
                .ForMember(dest => dest.EntreCalles, opt => opt.MapFrom(src => src.EntreCalles))
                .ForMember(dest => dest.roleName, opt => opt.MapFrom(src => src.roleName))
                .ForMember(dest => dest.PermisosEspeciales, opt => opt.MapFrom<permisosEspecialesToUsuarioRegistradoDtoResolver>())
                .ForMember(dest => dest.dniVerificado, opt => opt.MapFrom(src => src.dniVerificado));

            CreateMap<UsuarioRegistradoDTO, UsuarioRegistrado>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.DNI))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.Edad))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Calle, opt => opt.MapFrom(src => src.Calle))
                .ForMember(dest => dest.Altura, opt => opt.MapFrom(src => src.Altura))
                .ForMember(dest => dest.Dpto, opt => opt.MapFrom(src => src.Dpto))
                .ForMember(dest => dest.EntreCalles, opt => opt.MapFrom(src => src.EntreCalles))
                .ForMember(dest => dest.roleName, opt => opt.MapFrom(src => src.roleName))
                .ForMember(dest => dest.PermisosEspeciales, opt => opt.MapFrom<permisosEspecialesDtoToUsuarioRegistradoResolver>())
                .ForMember(dest => dest.dniVerificado, opt => opt.Ignore());
        }
    }
}
