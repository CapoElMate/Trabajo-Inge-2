using AutoMapper;
using Bussines_Logic_Layer.DTOs.Publicacion;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.Resolvers.Publicacion
{
    public class PoliticaDeCancelacionDtoToPublicacion : IValueResolver<PublicacionDto, Domain_Layer.Entidades.Publicacion , PoliticaDeCancelacion>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PoliticaDeCancelacionDtoToPublicacion(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public PoliticaDeCancelacion Resolve(PublicacionDto source, Domain_Layer.Entidades.Publicacion destination
                                           , PoliticaDeCancelacion destMember, ResolutionContext context)
        {

            var tipoExistente = _context.PoliticasDeCancelacion.FirstOrDefault(m => m.Politica.Equals(source.PoliticaDeCancelacion.Politica));

            if (tipoExistente == null)
            {
                throw new Exception("No existe una politica de cancelacion para los valores proporcionados");
            }

            return _mapper.Map<PoliticaDeCancelacion>(tipoExistente);
        }

    }
}
