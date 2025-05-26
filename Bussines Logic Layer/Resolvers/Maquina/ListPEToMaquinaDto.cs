using System;
using System.Linq;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers.Maquina
{
    public class ListPEToMaquinaDto : IValueResolver<Domain_Layer.Entidades.Maquina, MaquinaDto, ICollection<PermisoEspecialDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ListPEToMaquinaDto(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ICollection<PermisoEspecialDto> Resolve(Domain_Layer.Entidades.Maquina source, MaquinaDto destination, ICollection<PermisoEspecialDto> destMember, ResolutionContext context)
        {
            var permisosEspeciales = _context.PermisosEspeciales
                .Where(p => source.PermisosEspeciales.Select(pe => pe.Permiso).Contains(p.Permiso))
                .ToList();

            if (permisosEspeciales == null || !permisosEspeciales.Any())
            {
                throw new Exception("No existen permisos especiales para los valores proporcionados");
            }

            //return permisosEspeciales
            //    .Select(p => new PermisoEspecialDto { Permiso = p.Permiso})
            //    .ToList();
            return _mapper.Map<ICollection<PermisoEspecialDto>>(permisosEspeciales);
        }
    }
}
