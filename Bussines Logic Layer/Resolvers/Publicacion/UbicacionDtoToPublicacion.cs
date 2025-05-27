using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.Resolvers.Publicacion
{
    public class UbicacionDtoToPublicacion : IValueResolver<PublicacionDto, Domain_Layer.Entidades.Publicacion, Ubicacion>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UbicacionDtoToPublicacion(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Ubicacion Resolve(PublicacionDto source, Domain_Layer.Entidades.Publicacion destination
                                           , Ubicacion destMember, ResolutionContext context)
        {

            var tipoExistente = _context.Ubicaciones.FirstOrDefault(u => u.UbicacionName.Equals(source.Ubicacion.UbicacionName));

            if (tipoExistente == null)
            {
                throw new Exception("No existe una ubicacion para los valores proporcionados");
            }

            return _mapper.Map<Ubicacion>(tipoExistente);
        }

    }
}
