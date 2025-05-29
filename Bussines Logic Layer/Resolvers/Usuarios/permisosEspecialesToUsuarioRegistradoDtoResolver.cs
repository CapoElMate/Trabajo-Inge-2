using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers.Usuarios
{
    public class permisosEspecialesToUsuarioRegistradoDtoResolver : IValueResolver<UsuarioRegistrado, UsuarioRegistradoDTO, ICollection<PermisoEspecialUsuarioDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public permisosEspecialesToUsuarioRegistradoDtoResolver(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ICollection<PermisoEspecialUsuarioDto> Resolve(UsuarioRegistrado source, UsuarioRegistradoDTO destination, ICollection<PermisoEspecialUsuarioDto> destMember, ResolutionContext context)
        {
            var usuarioExistente = _context.UsuariosRegistrados.FirstOrDefault(u => u.DNI == source.DNI);

            if (usuarioExistente == null)
            {
                throw new Exception("El usuario no existe.");
            }
            //return usuarioExistente.PermisosEspeciales
            //        .Select(pe => new PermisoEspecialUsuarioDto
            //        {
            //            Permiso = pe.Permiso,
            //            status = pe.status,
            //            fecEmision = pe.fecEmision,
            //            fecVencimiento = pe.fecVencimiento
            //        })
            //        .ToList();
            return _mapper.Map<ICollection<PermisoEspecialUsuarioDto>>(usuarioExistente.PermisosEspeciales);
        }
    }
}
