using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping.Usuarios
{
    public class PermisoEspecialUsuario: Profile
    {
        public PermisoEspecialUsuario()
        {
            CreateMap<UsuarioRegistrado_PermisoEspecial, PermisoEspecialUsuarioDto>()
                .ForMember(dest => dest.fecEmision, opt => opt.MapFrom(src => src.fecEmision))
                .ForMember(dest => dest.fecVencimiento, opt => opt.MapFrom(src => src.fecVencimiento))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.Permiso, opt => opt.MapFrom(src => src.Permiso));
            CreateMap<PermisoEspecialUsuarioDto, UsuarioRegistrado_PermisoEspecial>()
                .ForMember(dest => dest.fecEmision, opt => opt.MapFrom(src => src.fecEmision))
                .ForMember(dest => dest.fecVencimiento, opt => opt.MapFrom(src => src.fecVencimiento))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.Permiso, opt => opt.MapFrom(src => src.Permiso));
        }
    }
}
