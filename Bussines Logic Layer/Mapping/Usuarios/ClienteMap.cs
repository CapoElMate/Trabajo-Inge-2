using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Bussines_Logic_Layer.Resolvers.Usuarios;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Mapping.Usuarios
{
    public class ClienteMap : Profile
    {
        public ClienteMap()
        {
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dest => dest.UsuarioRegistrado, opt => opt.MapFrom<UsuarioRegistradoToClienteDtoResolver>());

            CreateMap<ClienteDTO, Cliente>()
                .ForMember(dest => dest.UsuarioRegistrado, opt => opt.MapFrom<UsuarioRegistradoDtoToClienteResolver>())
                .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.UsuarioRegistrado.DNI));
        }
    }
}
