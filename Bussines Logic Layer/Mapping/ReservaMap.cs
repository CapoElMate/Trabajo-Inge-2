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
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Resolvers.Publicacion;
using Bussines_Logic_Layer.Resolvers.Publicacion.Create;
using Bussines_Logic_Layer.DTOs.Reserva;
using Bussines_Logic_Layer.Resolvers.Reserva;
using Bussines_Logic_Layer.DTOs.Usuarios;

namespace Bussines_Logic_Layer.Mapping
{
    class ReservaMap : Profile
    {

        public ReservaMap()
        {
            //           source -> destination
            CreateMap<Reserva, ReservaDto>()
                //         destination               <-      source
                .ForMember(dest => dest.idReserva, opt => opt.MapFrom(src => src.idReserva))
                .ForMember(dest => dest.TipoEntrega, opt => opt.MapFrom<TipoEntregaToReservaDto>())
                .ForMember(dest => dest.Pago, opt => opt.MapFrom<PagoToReservaDto>())
                //.ForMember(dest => dest.Alquiler, opt => opt.MapFrom<AlquilerToReservaDto>())
                //.ForMember(dest => dest.Cliente, opt => opt.MapFrom<ClienteToReservaDto>())
                //.ForMember(dest => dest.Publicacion, opt => opt.MapFrom<PublicacionToReservaDto>())
                .ForMember(dest => dest.fecInicio, opt => opt.MapFrom(src => src.fecInicio))
                .ForMember(dest => dest.fecFin, opt => opt.MapFrom(src => src.fecFin))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.montoTotal, opt => opt.MapFrom(src => src.montoTotal))
                .ForMember(dest => dest.Calle, opt => opt.MapFrom(src => src.Calle))
                .ForMember(dest => dest.Altura, opt => opt.MapFrom(src => src.Altura))
                .ForMember(dest => dest.Dpto, opt => opt.MapFrom(src => src.Dpto))
                .ForMember(dest => dest.EntreCalles, opt => opt.MapFrom(src => src.EntreCalles))
                .ForMember(dest => dest.idReserva, opt => opt.MapFrom(src => src.idReserva))
                .ForMember(dest => dest.IdAlquiler, opt => opt.MapFrom(src => src.idAlquiler))
                .ForMember(dest => dest.DNICliente, opt => opt.MapFrom(src => src.DNI));

            CreateMap<ReservaDto,Reserva>()
                .ForMember(dest => dest.idReserva, opt => opt.MapFrom(src => src.idReserva))
                .ForMember(dest => dest.TipoEntrega, opt => opt.MapFrom<TipoEntregaDtoToReserva>())
                .ForMember(dest => dest.Pago, opt => opt.MapFrom<PagoDtoToReserva>())
                .ForMember(dest => dest.Alquiler, opt => opt.MapFrom<AlquilerDtoToReserva>())
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom<ClienteDtoToReserva>())
                .ForMember(dest => dest.Publicacion, opt => opt.MapFrom<PublicacionDtoToReserva>())
                .ForMember(dest => dest.fecInicio, opt => opt.MapFrom(src => src.fecInicio))
                .ForMember(dest => dest.fecFin, opt => opt.MapFrom(src => src.fecFin))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.montoTotal, opt => opt.MapFrom(src => src.montoTotal))
                .ForMember(dest => dest.Calle, opt => opt.MapFrom(src => src.Calle))
                .ForMember(dest => dest.Altura, opt => opt.MapFrom(src => src.Altura))
                .ForMember(dest => dest.Dpto, opt => opt.MapFrom(src => src.Dpto))
                .ForMember(dest => dest.EntreCalles, opt => opt.MapFrom(src => src.EntreCalles));

            CreateMap<CreateReservaDto,Reserva>()
                .ForMember(dest => dest.TipoEntrega, opt => opt.MapFrom<CreateTipoEntregaDtoToReserva>())
                .ForMember(dest => dest.Pago, opt => opt.MapFrom<CreatePagoDtoToReserva>())
                .ForMember(dest => dest.Alquiler, opt => opt.MapFrom<CreateAlquilerDtoToReserva>())
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom<CreateClienteDtoToReserva>())
                .ForMember(dest => dest.Publicacion, opt => opt.MapFrom<CreatePublicacionDtoToReserva>())
                .ForMember(dest => dest.fecInicio, opt => opt.MapFrom(src => src.fecInicio))
                .ForMember(dest => dest.fecFin, opt => opt.MapFrom(src => src.fecFin))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.montoTotal, opt => opt.MapFrom(src => src.montoTotal))
                .ForMember(dest => dest.Calle, opt => opt.MapFrom(src => src.Calle))
                .ForMember(dest => dest.Altura, opt => opt.MapFrom(src => src.Altura))
                .ForMember(dest => dest.Dpto, opt => opt.MapFrom(src => src.Dpto))
                .ForMember(dest => dest.EntreCalles, opt => opt.MapFrom(src => src.EntreCalles));
        }
    }
}
