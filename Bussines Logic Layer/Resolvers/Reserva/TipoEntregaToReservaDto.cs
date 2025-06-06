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
    public class TipoEntregaToReservaDto : IValueResolver<Domain_Layer.Entidades.Reserva, ReservaDto, TipoEntregaDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoEntregaToReservaDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public TipoEntregaDto Resolve(Domain_Layer.Entidades.Reserva source, ReservaDto destination
                                    , TipoEntregaDto destMember, ResolutionContext context)
        {

            var tipoEntregaExistente = _context.TiposEntrega.FirstOrDefault(te => te.Entrega.Equals(source.Entrega));

            if (tipoEntregaExistente == null)
            {
                throw new Exception("No existe un tipo de entrega para los valores proporcionados");
            }

            return _mapper.Map<TipoEntregaDto>(tipoEntregaExistente);
        }

    }
}
