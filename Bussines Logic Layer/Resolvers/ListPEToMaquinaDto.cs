using System;
using System.Linq;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers
{
    public class ListPEToMaquinaDto : IValueResolver<Maquina, MaquinaDto, ICollection<PermisoEspecialDto>>
    {
        private readonly ApplicationDbContext _context;

        public ListPEToMaquinaDto(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<PermisoEspecialDto> Resolve(Maquina source, MaquinaDto destination, ICollection<PermisoEspecialDto> destMember, ResolutionContext context)
        {
            var permisosEspeciales = _context.PermisosEspeciales
                .Where(p => source.PermisosEspeciales.Select(pe => pe.Permiso).Contains(p.Permiso))
                .ToList();

            if (permisosEspeciales == null || !permisosEspeciales.Any())
            {
                throw new Exception("No existen permisos especiales para los valores proporcionados");
            }

            return permisosEspeciales
                .Select(p => new PermisoEspecialDto { Permiso = p.Permiso})
                .ToList();
        }
    }
}
