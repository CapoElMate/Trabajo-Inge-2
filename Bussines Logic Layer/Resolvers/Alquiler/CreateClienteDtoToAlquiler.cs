using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Data_Access_Layer;

namespace Bussines_Logic_Layer.Resolvers.Alquiler
{
    public class CreateClienteDtoToAlquiler : IValueResolver<CreateAlquilerDto, Domain_Layer.Entidades.Alquiler, Domain_Layer.Entidades.Cliente>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateClienteDtoToAlquiler(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Domain_Layer.Entidades.Cliente Resolve(CreateAlquilerDto source, Domain_Layer.Entidades.Alquiler destination
                                           , Domain_Layer.Entidades.Cliente destMember, ResolutionContext context)
        {

            var reservaExistente = _context.Clientes.FirstOrDefault(m => m.DNI.Equals(source.Reserva.DNICliente));

            if (reservaExistente == null)
            {
                throw new Exception("No existe cliente para los valores proporcionados");
            }

            return _mapper.Map<Domain_Layer.Entidades.Cliente>(reservaExistente);
        }

    }
}
