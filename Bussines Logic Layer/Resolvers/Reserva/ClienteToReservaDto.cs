using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;
using Bussines_Logic_Layer.DTOs.Reserva;
using Bussines_Logic_Layer.DTOs.Usuarios;

namespace Bussines_Logic_Layer.Resolvers.Reserva
{
    public class ClienteToReservaDto : IValueResolver<Domain_Layer.Entidades.Reserva, ReservaDto, ClienteDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClienteToReservaDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ClienteDto Resolve(Domain_Layer.Entidades.Reserva source, ReservaDto destination
                             , ClienteDto destMember, ResolutionContext context)
        {

            var clienteExistente = _context.Clientes.FirstOrDefault(c => c.DNI.Equals(source.Cliente.DNI));

            if (clienteExistente == null)
            {
                throw new Exception("No existe un cliente para los valores proporcionados");
            }

            return _mapper.Map<ClienteDto>(clienteExistente);
        }

    }
}
