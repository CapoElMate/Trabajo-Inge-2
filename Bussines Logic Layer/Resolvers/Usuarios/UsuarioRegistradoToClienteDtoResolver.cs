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
    public class UsuarioRegistradoToClienteDtoResolver : IValueResolver<Cliente, DTOs.Usuarios.ClienteDto, UsuarioRegistradoDTO>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioRegistradoToClienteDtoResolver(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UsuarioRegistradoDTO Resolve(Cliente source, DTOs.Usuarios.ClienteDto destination, UsuarioRegistradoDTO destMember, ResolutionContext context)
        {
            var usuarioExistente = _context.UsuariosRegistrados.FirstOrDefault(u => u.DNI == source.UsuarioRegistrado.DNI);

            if (usuarioExistente == null)
            {
                throw new Exception("El usuario no existe.");
            }
            //return new UsuarioRegistradoDTO
            //{
            //    Email = usuarioExistente.Email,
            //    DNI = usuarioExistente.DNI,
            //    Nombre = usuarioExistente.Nombre,
            //    Apellido = usuarioExistente.Apellido,
            //    Edad = usuarioExistente.Edad,
            //    Telefono = usuarioExistente.Telefono,
            //    Calle = usuarioExistente.Calle,
            //    Altura = usuarioExistente.Altura,
            //    Dpto = usuarioExistente.Dpto,
            //    EntreCalles = usuarioExistente.EntreCalles,
            //    roleName = usuarioExistente.roleName,
            //    PermisosEspeciales = usuarioExistente.PermisosEspeciales
            //        .Select(pe => new PermisoEspecialUsuarioDto
            //        {
            //            Permiso = pe.Permiso,
            //            status = pe.status,
            //            fecEmision = pe.fecEmision,
            //            fecVencimiento = pe.fecVencimiento
            //        })
            //        .ToList()
            //};
            return _mapper.Map<UsuarioRegistradoDTO>(usuarioExistente);
        }
    }
}
