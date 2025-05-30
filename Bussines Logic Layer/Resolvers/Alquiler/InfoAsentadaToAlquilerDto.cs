using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Bussines_Logic_Layer.DTOs.InfoAsentada;
using Data_Access_Layer;

namespace Bussines_Logic_Layer.Resolvers.Alquiler
{
    public class InfoAsentadaToAlquilerDto : IValueResolver<Domain_Layer.Entidades.Alquiler, AlquilerDto, ICollection<InfoAsentadaDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InfoAsentadaToAlquilerDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ICollection<InfoAsentadaDto> Resolve(Domain_Layer.Entidades.Alquiler source, AlquilerDto destination
                                                        , ICollection<InfoAsentadaDto> destMember, ResolutionContext context)
        {
            // Si no se proporcionan tags de Alquiler en el DTO, retornar una lista vacía
            if (source.InfoAsentada == null || !source.InfoAsentada.Any())
            {
                return new List<InfoAsentadaDto>();
            }

            var infoAsentada = _context.InfoAsentada
                .Where(i => source.InfoAsentada.Select(si => si.idInfo).Contains(i.idInfo))
                .ToList();

            if (infoAsentada == null || !infoAsentada.Any())
            {
                throw new Exception("No existe informacion asentada para el alquiler proporcionado");
            }

            return _mapper.Map<ICollection<InfoAsentadaDto>>(infoAsentada);
        }
    }
}
