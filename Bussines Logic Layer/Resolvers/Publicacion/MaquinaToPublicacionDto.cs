using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.Resolvers.Publicacion
{
    public class MaquinaToPublicacionDto : IValueResolver<Domain_Layer.Entidades.Publicacion, PublicacionDto, MaquinaDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MaquinaToPublicacionDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public MaquinaDto Resolve(Domain_Layer.Entidades.Publicacion source, PublicacionDto destination
                                                       , MaquinaDto destMember, ResolutionContext context)
        {
            var publicacion = _context.Publicaciones
                .Where(p => source.Maquina.idMaquina == p.idMaquina)
                .ToList();

            if (publicacion == null || !publicacion.Any())
            {
                throw new Exception("No existen permisos especiales para los valores proporcionados");
            }


            return _mapper.Map<MaquinaDto>(publicacion);
        }
    }
    
}
