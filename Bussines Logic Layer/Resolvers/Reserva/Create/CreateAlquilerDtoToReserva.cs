using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Reserva;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers.Reserva
{
    public class CreateAlquilerDtoToReserva : IValueResolver<CreateReservaDto, Domain_Layer.Entidades.Reserva, Domain_Layer.Entidades.Alquiler>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAlquilerDtoToReserva(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Domain_Layer.Entidades.Alquiler Resolve(CreateReservaDto source, Domain_Layer.Entidades.Reserva destination
                                           , Domain_Layer.Entidades.Alquiler destMember, ResolutionContext context)
        {

            var tipoExistente = _context.Alquileres.FirstOrDefault(a => a.idAlquiler.Equals(source.IdAlquiler));

            if (tipoExistente == null)
            {
                return null;
                ///throw new Exception("No existe un alquiler para los valores proporcionados");
            }

            return _mapper.Map<Domain_Layer.Entidades.Alquiler>(tipoExistente);
        }
    }
}
