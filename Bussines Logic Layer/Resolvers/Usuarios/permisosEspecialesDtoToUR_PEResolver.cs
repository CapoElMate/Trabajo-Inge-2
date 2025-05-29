using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers.Usuarios
{
    public class permisosEspecialesDtoToUR_PEResolver : IValueResolver<PermisoEspecialUsuarioDto, UsuarioRegistrado_PermisoEspecial, PermisoEspecial>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public permisosEspecialesDtoToUR_PEResolver(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PermisoEspecial Resolve(PermisoEspecialUsuarioDto source, UsuarioRegistrado_PermisoEspecial destination, PermisoEspecial destMember, ResolutionContext context)
        {
            var usuarioExistente = _context.UsuarioRegistrado_PermisoEspecial.FirstOrDefault(u => u.UsuarioRegistradoDNI == source.DNICliente);

            if (usuarioExistente == null)
            {
                throw new Exception("El usuario no existe");
            }

            //_mapper.Map(source.PermisosEspeciales, usuarioExistente.PermisosEspeciales);
            return usuarioExistente.PermisoEspecial;
        }
    }
}
