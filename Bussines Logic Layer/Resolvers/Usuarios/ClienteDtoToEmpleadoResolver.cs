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
    public class ClienteDtoToEmpeladoResolver : IValueResolver<EmpleadoDTO, Empleado, Cliente>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClienteDtoToEmpeladoResolver(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Cliente Resolve(EmpleadoDTO source, Empleado destination, Cliente destMember, ResolutionContext context)
        {
            var clienteExistente = _context.Clientes.FirstOrDefault(u => u.DNI == source.Cliente.UsuarioRegistrado.DNI);

            if (clienteExistente == null)
            {
                throw new Exception("El cliente no existe");
            }

            _mapper.Map(source.Cliente, clienteExistente);
            return clienteExistente;
        }
    }
}
