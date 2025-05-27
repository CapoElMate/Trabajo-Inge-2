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
    public class TipoEntregaDtoToReserva : IValueResolver<ReservaDto, Domain_Layer.Entidades.Reserva, TipoEntrega>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoEntregaDtoToReserva(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public TipoEntrega Resolve(ReservaDto source, Domain_Layer.Entidades.Reserva destination
                                           , TipoEntrega destMember, ResolutionContext context)
        {

            var tipoExistente = _context.TiposEntrega.FirstOrDefault(te => te.Entrega.Equals(source.TipoEntrega.Entrega));

            if (tipoExistente == null)
            {
                throw new Exception("No existe un tipo de entrega para los valores proporcionados");
            }

            return _mapper.Map<TipoEntrega>(tipoExistente);
        }
    }
}
