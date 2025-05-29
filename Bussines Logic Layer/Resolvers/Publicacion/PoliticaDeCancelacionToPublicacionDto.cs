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
using Bussines_Logic_Layer.DTOs.Publicacion;

namespace Bussines_Logic_Layer.Resolvers.Publicacion
{
    public class PoliticaDeCancelacionToPublicacionDto : IValueResolver<Domain_Layer.Entidades.Publicacion, PublicacionDto, PoliticaDeCancelacionDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PoliticaDeCancelacionToPublicacionDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public PoliticaDeCancelacionDto Resolve(Domain_Layer.Entidades.Publicacion source, PublicacionDto destination
                                                       , PoliticaDeCancelacionDto destMember, ResolutionContext context)
        {

            var politicaExistente = _context.PoliticasDeCancelacion.FirstOrDefault(p => p.Politica.Equals(source.PoliticaDeCancelacion.Politica));

            if (politicaExistente == null)
            {
                throw new Exception("No existe una politica de cancelacion para los valores proporcionados");
            }

            return _mapper.Map<PoliticaDeCancelacionDto>(politicaExistente);
        }

    }
}
