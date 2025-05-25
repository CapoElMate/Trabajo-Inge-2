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
    public class PermisoEspecialMap : Profile
    {
        public PermisoEspecialMap()
        {
            CreateMap<PermisoEspecial, PermisoEspecialDto>()
                .ForMember(dest => dest.Permiso, opt => opt.MapFrom(src => src.Permiso));
            CreateMap<PermisoEspecialDto, PermisoEspecial>()
                .ForMember(dest => dest.Permiso, opt => opt.MapFrom(src => src.Permiso));
        }
    }
}
