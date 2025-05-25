using System;
using System.Linq;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Maquina;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers
{
    public class ListPEToMaquina : IValueResolver<CreateMaquinaDto, Maquina, ICollection<PermisoEspecial>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ListPEToMaquina(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<PermisoEspecial> Resolve(CreateMaquinaDto source, Maquina destination, ICollection<PermisoEspecial> destMember, ResolutionContext context)
        {
            var permisosEspeciales = _context.PermisosEspeciales
                .Where(p => source.PermisosEspeciales.Select(pe => pe.Permiso).Contains(p.Permiso))
                .ToList();

            if (permisosEspeciales == null || !permisosEspeciales.Any())
            {
                throw new Exception("No existen permisos especiales para los valores proporcionados");
            }

            _mapper.Map(source.PermisosEspeciales, permisosEspeciales);
            return permisosEspeciales;
        }
    }
}
