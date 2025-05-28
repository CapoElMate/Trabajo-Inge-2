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

namespace Bussines_Logic_Layer.Resolvers.Reserva
{
    public class AlquilerToReservaDto : IValueResolver<Domain_Layer.Entidades.Reserva, ReservaDto, AlquilerDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AlquilerToReservaDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public AlquilerDto Resolve(Domain_Layer.Entidades.Reserva source, ReservaDto destination
                                    , AlquilerDto destMember, ResolutionContext context)
        {

            var alquilerExistente = _context.Alquileres.FirstOrDefault(a => a.idAlquiler.Equals(source.Alquiler.idAlquiler));

            if (alquilerExistente == null)
            {
                throw new Exception("No existe un alquiler para los valores proporcionados");
            }

            return _mapper.Map<AlquilerDto>(alquilerExistente);
        }

    }
}
