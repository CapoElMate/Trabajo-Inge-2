using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Publicacion;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.Resolvers.Publicacion
{
    public class UbicacionToPublicacionDto : IValueResolver<Domain_Layer.Entidades.Publicacion, PublicacionDto, UbicacionDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UbicacionToPublicacionDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public UbicacionDto Resolve(Domain_Layer.Entidades.Publicacion source, PublicacionDto destination
                                                        , UbicacionDto destMember, ResolutionContext context)
        {

            var tipoExistente = _context.Ubicaciones.FirstOrDefault(u => u.UbicacionName.Equals(source.Ubicacion.UbicacionName));

            if (tipoExistente == null)
            {
                throw new Exception("No existe una ubicacion para los valores proporcionados");
            }

            return _mapper.Map<UbicacionDto>(tipoExistente);
        }

    }

}
