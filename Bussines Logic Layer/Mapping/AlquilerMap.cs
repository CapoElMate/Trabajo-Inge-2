using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Bussines_Logic_Layer.Resolvers.Alquiler;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping
{
    public class AlquilerMap: Profile
    {
        public AlquilerMap() {
            CreateMap<Alquiler, AlquilerDto>()
                .ForMember(dest => dest.DNIEmpleadoEfectivizo, opt => opt.MapFrom(src => src.DNIEmpleado))
                .ForMember(dest => dest.InfoAsentada, opt => opt.MapFrom<InfoAsentadaToAlquilerDto>())
                .ForMember(dest => dest.Reserva, opt => opt.MapFrom<ReservaToAlquilerDto>());
            CreateMap<AlquilerDto, Alquiler>()
                .ForMember(dest => dest.DNIEmpleado, opt => opt.MapFrom(src => src.DNIEmpleadoEfectivizo))
                .ForMember(dest => dest.InfoAsentada, opt => opt.MapFrom<InfoAsentadaDtoToAlquiler>())
                .ForMember(dest => dest.Reserva, opt => opt.MapFrom<ReservaDtoToAlquiler>());
            CreateMap<CreateAlquilerDto, Alquiler>()
                .ForMember(dest => dest.DNIEmpleado, opt => opt.MapFrom(src => src.DNIEmpleadoEfectivizo))
                .ForMember(dest => dest.InfoAsentada, opt => opt.MapFrom<CreateInfoAsentadaDtoToAlquiler>())
                .ForMember(dest => dest.Reserva, opt => opt.MapFrom<CreateReservaDtoToAlquiler>())
                .ForMember(dest => dest.idReserva, opt => opt.MapFrom(src => src.Reserva.idReserva))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom<CreateClienteDtoToAlquiler>())
                .ForMember(dest => dest.Empleado, opt => opt.MapFrom<CreateEmpleadoDtoToAlquiler>());
        }
    }
}
