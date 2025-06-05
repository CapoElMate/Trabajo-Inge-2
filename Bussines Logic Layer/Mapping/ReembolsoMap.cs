using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Reembolso;
using Bussines_Logic_Layer.Resolvers.Publicacion;

namespace Bussines_Logic_Layer.Mapping
{
    public class ReembolsoMap: Profile
    {
        public ReembolsoMap() 
        {
            CreateMap<Domain_Layer.Entidades.Reembolso, ReembolsoDto>()
                .ForMember(dest => dest.idAlquiler, opt => opt.MapFrom(src => src.idAlquiler))
                .ForMember(dest => dest.DNICliente, opt => opt.MapFrom(src => src.Cliente.DNI))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto))
                .ForMember(dest => dest.Motivo, opt => opt.MapFrom(src => src.Motivo))
                .ForMember(dest => dest.fecCancelacion, opt => opt.MapFrom(src => src.fecCancelacion))
                .ForMember(dest => dest.idReembolso, opt => opt.MapFrom(src => src.idReembolso));

            CreateMap<ReembolsoDto, Domain_Layer.Entidades.Reembolso>()
                .ForMember(dest => dest.idReembolso, opt => opt.MapFrom(src => src.idReembolso))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto))
                .ForMember(dest => dest.Motivo, opt => opt.MapFrom(src => src.Motivo))
                .ForMember(dest => dest.DNICliente, opt => opt.MapFrom(src => src.DNICliente))
                .ForMember(dest => dest.fecCancelacion, opt => opt.MapFrom(src => src.fecCancelacion))
                .ForMember(dest => dest.idAlquiler, opt => opt.MapFrom(src => src.idAlquiler));
            
            CreateMap<CreateReembolsoDto, Domain_Layer.Entidades.Reembolso>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto))
                .ForMember(dest => dest.Motivo, opt => opt.MapFrom(src => src.Motivo))
                .ForMember(dest => dest.DNICliente, opt => opt.MapFrom(src => src.DNICliente))
                .ForMember(dest => dest.fecCancelacion, opt => opt.MapFrom(src => src.fecCancelacion))
                .ForMember(dest => dest.idAlquiler, opt => opt.MapFrom(src => src.idAlquiler));
        }
    }
}
