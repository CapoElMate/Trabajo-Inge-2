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
    public class CreatePagoDtoToReserva : IValueResolver<CreateReservaDto, Domain_Layer.Entidades.Reserva , Pago>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePagoDtoToReserva(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Pago Resolve(CreateReservaDto source, Domain_Layer.Entidades.Reserva destination
                                           , Pago destMember, ResolutionContext context)
        {

            //var tipoExistente = _context.Pagos.FirstOrDefault(p => p.nroPago.Equals(source.Pago.nroPago));

            //if (tipoExistente == null)
            //{
            //    throw new Exception("No existe un pago para los valores proporcionados");
            //}

            //return _mapper.Map<Pago>(tipoExistente);
            return null;
        }
    }
}
