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
using Bussines_Logic_Layer.Resolvers.Maquina.Create;
using Bussines_Logic_Layer.Resolvers.Maquina;

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
                .ForMember(dest => dest.TipoMaquina, opt => opt.MapFrom<TMToMaquinaDto>())
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom<ModeloToMaquinaDto>())
                .ForMember(dest => dest.AnioFabricacion, opt => opt.MapFrom(src => src.anioFabricacion))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status));

            CreateMap<MaquinaDto, Maquina>()
                .ForMember(dest => dest.idMaquina, opt => opt.MapFrom(src => src.IdMaquina))
                .ForMember(dest => dest.PermisosEspeciales, opt => opt.MapFrom<ListPETMaquinaoMaquina>())
                .ForMember(dest => dest.TagsMaquina, opt => opt.MapFrom<ListTAMaquinaToMaquina>())
                .ForMember(dest => dest.TipoMaquina, opt => opt.MapFrom<TMMaquinaToMaquinaResolver>())
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom<ModeloMaquinaToMaquinaResolver>())
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoMaquina.Tipo))
                .ForMember(dest => dest.ModeloName, opt => opt.MapFrom(src => src.Modelo.Modelo))
                .ForMember(dest => dest.anioFabricacion, opt => opt.MapFrom(src => src.AnioFabricacion))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));

            CreateMap<CreateMaquinaDto, Maquina>()
                .ForMember(dest => dest.PermisosEspeciales, opt => opt.MapFrom<ListPETCreateMaquinaoMaquina>())
                .ForMember(dest => dest.TagsMaquina, opt => opt.MapFrom<ListTACreateMaquinaToMaquina>())
                .ForMember(dest => dest.TipoMaquina, opt => opt.MapFrom<TMCreateMaquinaToMaquina>())
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom<ModeloCreateMaquinaToMaquinaResolver>())
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoMaquina.Tipo))
                .ForMember(dest => dest.ModeloName, opt => opt.MapFrom(src => src.Modelo.Modelo))
                .ForMember(dest => dest.anioFabricacion, opt => opt.MapFrom(src => src.AnioFabricacion))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));

        }
    }
}
