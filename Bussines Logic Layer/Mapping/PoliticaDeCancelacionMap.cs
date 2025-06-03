using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Resolvers.Publicacion;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping
{
    public class PoliticaDeCancelacionMap:Profile
    {
        public PoliticaDeCancelacionMap()
        {
            CreateMap<PoliticaDeCancelacion, PoliticaDeCancelacionDto>()
                .ForMember(dest => dest.Politica, opt => opt.MapFrom(src => src.Politica))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

            CreateMap<PoliticaDeCancelacionDto, PoliticaDeCancelacion>()
               .ForMember(dest => dest.Politica, opt => opt.MapFrom(src => src.Politica))
               .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

            /*
             CreateMap<Maquina, MaquinaDto>()
                .ForMember(dest => dest.IdMaquina, opt => opt.MapFrom(src => src.idMaquina))
                .ForMember(dest => dest.PermisosEspeciales, opt => opt.MapFrom<ListPEToMaquinaDto>())
                .ForMember(dest => dest.TagsMaquina, opt => opt.MapFrom<ListTAToMaquinaDto>())
                .ForMember(dest => dest.TipoMaquina, opt => opt.MapFrom<TMToMaquinaDto>())
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom<ModeloToMaquinaDto>())
                .ForMember(dest => dest.AnioFabricacion, opt => opt.MapFrom(src => src.anioFabricacion))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status));

             */

            /*
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
             */

        }
    }
}
