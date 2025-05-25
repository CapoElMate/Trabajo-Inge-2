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
    public class permisosEspecialesDtoToUsuarioRegistradoResolver : IValueResolver<UsuarioRegistradoDTO, UsuarioRegistrado, ICollection<UsuarioRegistrado_PermisoEspecial>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public permisosEspecialesDtoToUsuarioRegistradoResolver(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<UsuarioRegistrado_PermisoEspecial> Resolve(UsuarioRegistradoDTO source, UsuarioRegistrado destination, ICollection<UsuarioRegistrado_PermisoEspecial> destMember, ResolutionContext context)
        {
            var usuarioExistente = _context.UsuariosRegistrados.FirstOrDefault(u => u.DNI == source.DNI);

            if (usuarioExistente == null)
            {
                throw new Exception("El usuario no existe");
            }

            //_mapper.Map(source.PermisosEspeciales, usuarioExistente.PermisosEspeciales);
            return usuarioExistente.PermisosEspeciales;
        }
    }
}
