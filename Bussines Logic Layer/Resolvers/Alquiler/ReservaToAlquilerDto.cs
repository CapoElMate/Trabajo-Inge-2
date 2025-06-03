using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Publicacion;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Bussines_Logic_Layer.DTOs.Reserva;

namespace Bussines_Logic_Layer.Resolvers.Alquiler
{
    public class ReservaToAlquilerDto : IValueResolver<Domain_Layer.Entidades.Alquiler, AlquilerDto, ReservaDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReservaToAlquilerDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ReservaDto Resolve(Domain_Layer.Entidades.Alquiler source, AlquilerDto destination
                                                       , ReservaDto destMember, ResolutionContext context)
        {
            var reservsaExistente = _context.Reservas.FirstOrDefault(p => p.idReserva.Equals(source.idReserva));

            if (reservsaExistente == null)
            {
                throw new Exception("No existe una resreva para los valores proporcionados");
            }

            return _mapper.Map<ReservaDto>(reservsaExistente);
        }

    }
}
