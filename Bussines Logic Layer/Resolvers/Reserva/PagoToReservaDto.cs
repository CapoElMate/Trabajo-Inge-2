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
    public class PagoToReservaDto : IValueResolver<Domain_Layer.Entidades.Reserva, ReservaDto, PagoDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PagoToReservaDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public PagoDto Resolve(Domain_Layer.Entidades.Reserva source, ReservaDto destination
                                    , PagoDto destMember, ResolutionContext context)
        {

            var pagoExistente = _context.Pagos.FirstOrDefault(p => p.nroPago.Equals(source.Pago.nroPago));

            if (pagoExistente == null)
            {
                throw new Exception("No existe un pago para los valores proporcionados");
            }

            return _mapper.Map<PagoDto>(pagoExistente);
        }

    }
}
