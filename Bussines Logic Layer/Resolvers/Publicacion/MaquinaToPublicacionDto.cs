using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
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
            var maquinaExistente = _context.Maquinas.Include(m => m.Modelo)
                .Include(m => m.PermisosEspeciales)
                .Include(m => m.Modelo.Marca)
                .Include(m => m.TipoMaquina)
                .Include(m => m.TagsMaquina)
                .FirstOrDefault(m => m.idMaquina.Equals(source.Maquina.idMaquina));

            if (maquinaExistente == null)
            {
                throw new Exception("La publicacion no existe");
            }

            return _mapper.Map<MaquinaDto>(maquinaExistente);
        }
    }
    
}
