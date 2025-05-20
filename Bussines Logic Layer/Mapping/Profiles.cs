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
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Maquina, MaquinaDto>()
                .ForMember(dest => dest.IdMaquina, opt => opt.MapFrom(src => src.idMaquina));
            CreateMap<CreateMaquinaDto, Maquina>();

            CreateMap<Marca, MarcaDto>()
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.MarcaName));
            CreateMap<MarcaDto, Marca>()
                .ForMember(dest => dest.MarcaName, opt => opt.MapFrom(src => src.Marca));

            CreateMap<Modelo, ModeloDto>()
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.ModeloName));
            CreateMap<ModeloDto, Modelo>()
                .ForMember(dest => dest.ModeloName, opt => opt.MapFrom(src => src.Modelo));

            CreateMap<TipoMaquina, TipoMaquinaDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));
            CreateMap<TipoMaquinaDto, TipoMaquina>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));

            CreateMap<TagMaquina, TagMaquinaDto>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));
            CreateMap<TagMaquinaDto, TagMaquina>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));

            CreateMap<PermisoEspecial, PermisoEspecialDto>()
                .ForMember(dest => dest.Permiso, opt => opt.MapFrom(src => src.Permiso));
            CreateMap<PermisoEspecialDto, PermisoEspecial>()
                .ForMember(dest => dest.Permiso, opt => opt.MapFrom(src => src.Permiso));
        }
    }
}
