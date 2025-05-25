using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers.Usuarios
{
    public class UsuarioRegistradoDtoToClienteResolver : IValueResolver<ClienteDTO, Cliente, UsuarioRegistrado>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioRegistradoDtoToClienteResolver(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UsuarioRegistrado Resolve(ClienteDTO source, Cliente destination, UsuarioRegistrado destMember, ResolutionContext context)
        {
            var usuarioExistente = _context.UsuariosRegistrados.FirstOrDefault(u => u.DNI == source.UsuarioRegistrado.DNI);

            if (usuarioExistente == null)
            {
                throw new Exception("El tipo no existe");
            }

            //_mapper.Map(source.UsuarioRegistrado, usuarioExistente);
            return usuarioExistente;
        }
    }
}
