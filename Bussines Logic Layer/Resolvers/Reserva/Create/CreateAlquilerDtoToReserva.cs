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
    public class CreateAlquilerDtoToReserva : IValueResolver<CreateReservaDto, Domain_Layer.Entidades.Reserva, Alquiler>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAlquilerDtoToReserva(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Alquiler Resolve(CreateReservaDto source, Domain_Layer.Entidades.Reserva destination
                                           , Alquiler destMember, ResolutionContext context)
        {

            var tipoExistente = _context.Alquileres.FirstOrDefault(a => a.idAlquiler.Equals(source.Alquiler.idAlquiler));

            if (tipoExistente == null)
            {
                throw new Exception("No existe un alquiler para los valores proporcionados");
            }

            return _mapper.Map<Alquiler>(tipoExistente);
        }
    }
}
