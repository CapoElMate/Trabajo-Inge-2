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
    public class UR_PEToPermisosEspecialesUsuarioDtoResolver : IValueResolver<UsuarioRegistrado_PermisoEspecial, PermisoEspecialUsuarioDto, PermisoEspecialDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UR_PEToPermisosEspecialesUsuarioDtoResolver(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public PermisoEspecialDto Resolve(UsuarioRegistrado_PermisoEspecial source, PermisoEspecialUsuarioDto destination, PermisoEspecialDto destMember, ResolutionContext context)
        {
            var usuarioExistente = _context.UsuarioRegistrado_PermisoEspecial.FirstOrDefault(u => u.UsuarioRegistradoDNI == source.UsuarioRegistradoDNI);

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
            return _mapper.Map<PermisoEspecialDto>(usuarioExistente.PermisoEspecial);
        }
    }
}
