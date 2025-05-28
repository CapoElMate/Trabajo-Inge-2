using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Reserva;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.Resolvers.Reserva
{
    public class ClienteDtoToReserva : IValueResolver<ReservaDto, Domain_Layer.Entidades.Reserva , Cliente>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClienteDtoToReserva(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Cliente Resolve(ReservaDto source, Domain_Layer.Entidades.Reserva destination
                                           , Cliente destMember, ResolutionContext context)
        {

            var tipoExistente = _context.Clientes.FirstOrDefault(c => c.DNI.Equals(source.Cliente.UsuarioRegistrado.DNI));

            if (tipoExistente == null)
            {
                throw new Exception("No existe un cliente para los valores proporcionados");
            }

            return _mapper.Map<Cliente>(tipoExistente);
        }
    }
}
