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
    class ReservaDtoToAlquiler : IValueResolver<AlquilerDto, Domain_Layer.Entidades.Alquiler, Domain_Layer.Entidades.Reserva>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReservaDtoToAlquiler(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Domain_Layer.Entidades.Reserva Resolve(AlquilerDto source, Domain_Layer.Entidades.Alquiler destination
                                           , Domain_Layer.Entidades.Reserva destMember, ResolutionContext context)
        {

            var reservaExistente = _context.Reservas.FirstOrDefault(m => m.idReserva.Equals(source.Reserva.idReserva));

            if (reservaExistente == null)
            {
                throw new Exception("No existe una resreva para los valores proporcionados");
            }

            return _mapper.Map<Domain_Layer.Entidades.Reserva>(reservaExistente);
        }

    }
}
