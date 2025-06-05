using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.Resolvers.Maquina.Create;
using Bussines_Logic_Layer.Resolvers;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.Resolvers.Publicacion;
using Bussines_Logic_Layer.Resolvers.Publicacion.Create;
using Bussines_Logic_Layer.DTOs.Publicacion;

namespace Bussines_Logic_Layer.Mapping
{
    class PublicacionMap : Profile
    {

        public PublicacionMap()
        {
            //           source -> destination
            CreateMap<Publicacion, PublicacionDto>()
                //         destination               <-      source
                .ForMember(dest => dest.idPublicacion, opt => opt.MapFrom(src => src.idPublicacion))                                                                  
                .ForMember(dest => dest.Maquina, opt => opt.MapFrom<MaquinaToPublicacionDto>())
                .ForMember(dest => dest.TagsPublicacion, opt => opt.MapFrom<ListTagAPublicacionDto>())
                .ForMember(dest => dest.PoliticaDeCancelacion, opt => opt.MapFrom<PoliticaDeCancelacionToPublicacionDto>())
                .ForMember(dest => dest.Ubicacion, opt => opt.MapFrom<UbicacionToPublicacionDto>());

            CreateMap<PublicacionDto,Publicacion>()
                .ForMember(dest => dest.idPublicacion, opt => opt.MapFrom(src => src.idPublicacion))
                .ForMember(dest => dest.Maquina, opt => opt.MapFrom<MaquinaDtoToPublicacion>())
                .ForMember(dest => dest.TagsPublicacion, opt => opt.MapFrom<ListTagDtoAPublicacion>())
                .ForMember(dest => dest.PoliticaDeCancelacion, opt => opt.MapFrom<PoliticaDeCancelacionDtoToPublicacion>())
                .ForMember(dest => dest.Ubicacion, opt => opt.MapFrom<UbicacionDtoToPublicacion>());

            CreateMap<CreatePublicacionDto, Publicacion>()
                .ForMember(dest => dest.Maquina, opt => opt.MapFrom<CreateMaquinaDtoToPublicacion>())
                .ForMember(dest => dest.TagsPublicacion, opt => opt.MapFrom<CreateListTagDtoAPublicacion>())
                .ForMember(dest => dest.PoliticaDeCancelacion, opt => opt.MapFrom<CreatePoliticaDeCancelacionDtoToPublicacion>())
                .ForMember(dest => dest.Ubicacion, opt => opt.MapFrom<CreateUbicacionDtoToPublicacion>());
        }
    }
}
