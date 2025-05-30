using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.Resolvers;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping
{
    //public class Profiles : Profile
    //{
    //    public Profiles()
    //    {
    //        CreateMap<Maquina, MaquinaDto>()
    //            .ForMember(dest => dest.IdMaquina, opt => opt.MapFrom(src => src.idMaquina))
    //            .ForMember(dest => dest.PermisosEspeciales, opt => opt.MapFrom<ListPEToMaquinaDto>())
    //            .ForMember(dest => dest.TagsMaquina, opt => opt.MapFrom<ListTAToMaquinaDto>())
    //            .ForMember(dest => dest.TipoMaquina, opt => opt.MapFrom<TMToMaquinaDto>());

    //        CreateMap<CreateMaquinaDto, Maquina>()
    //            .ForMember(dest => dest.PermisosEspeciales, opt => opt.MapFrom<ListPEToMaquina>())
    //            .ForMember(dest => dest.TagsMaquina, opt => opt.MapFrom<ListTAToMaquina>())
    //            .ForMember(dest => dest.TipoMaquina, opt => opt.MapFrom<TMToMaquina>())
    //            .ForMember(dest => dest.Modelo, opt => opt.MapFrom<ModeloMToMaquinaResolver>())
    //            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoMaquina.Tipo))
    //            .ForMember(dest => dest.ModeloName, opt => opt.MapFrom(src => src.Modelo.Modelo));

    //        CreateMap<Marca, MarcaDto>()
    //            .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.MarcaName));
    //        CreateMap<MarcaDto, Marca>()
    //            .ForMember(dest => dest.MarcaName, opt => opt.MapFrom(src => src.Marca));

    //        CreateMap<Modelo, ModeloDto>()
    //            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.ModeloName))
    //            .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca));

    //        CreateMap<ModeloDto, Modelo>()
    //            .ForMember(dest => dest.ModeloName, opt => opt.MapFrom(src => src.Modelo))
    //            .ForMember(dest => dest.Marca, opt => opt.MapFrom<ModeloDtoToModeloResolver>())
    //            .ForMember(dest => dest.MarcaName, opt => opt.MapFrom(src => src.Marca.Marca));

    //        CreateMap<TipoMaquina, TipoMaquinaDto>()
    //            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));
    //        CreateMap<TipoMaquinaDto, TipoMaquina>()
    //            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));

    //        CreateMap<TagMaquina, TagMaquinaDto>()
    //            .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));
    //        CreateMap<TagMaquinaDto, TagMaquina>()
    //            .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));

    //        CreateMap<PermisoEspecial, PermisoEspecialDto>()
    //            .ForMember(dest => dest.Permiso, opt => opt.MapFrom(src => src.Permiso));
    //        CreateMap<PermisoEspecialDto, PermisoEspecial>()
    //            .ForMember(dest => dest.Permiso, opt => opt.MapFrom(src => src.Permiso));
    //    }
    //}
}
