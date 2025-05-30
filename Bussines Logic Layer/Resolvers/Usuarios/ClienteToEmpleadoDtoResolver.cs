using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Resolvers.Usuarios
{
    public class ClienteToEmpleadoDtoResolver : IValueResolver<Empleado, EmpleadoDto, DTOs.Usuarios.ClienteDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClienteToEmpleadoDtoResolver(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DTOs.Usuarios.ClienteDto Resolve(Empleado source, EmpleadoDto destination, DTOs.Usuarios.ClienteDto destMember, ResolutionContext context)
        {
            var clienteExistente = _context.Clientes.FirstOrDefault(u => u.DNI == source.Cliente.DNI);

            if (clienteExistente == null)
            {
                throw new Exception("El cliente no existe.");
            }



            return new DTOs.Usuarios.ClienteDto
            {
                //UsuarioRegistrado = new UsuarioRegistradoDTO {
                //    Email = clienteExistente.UsuarioRegistrado.Email,
                //    DNI = clienteExistente.UsuarioRegistrado.DNI,
                //    Nombre = clienteExistente.UsuarioRegistrado.Nombre,
                //    Apellido = clienteExistente.UsuarioRegistrado.Apellido,
                //    Edad = clienteExistente.UsuarioRegistrado.Edad,
                //    Telefono = clienteExistente.UsuarioRegistrado.Telefono,
                //    Calle = clienteExistente.UsuarioRegistrado.Calle,
                //    Altura = clienteExistente.UsuarioRegistrado.Altura,
                //    Dpto = clienteExistente.UsuarioRegistrado.Dpto,
                //    EntreCalles = clienteExistente.UsuarioRegistrado.EntreCalles,
                //    roleName = clienteExistente.UsuarioRegistrado.roleName,
                //    PermisosEspeciales = clienteExistente.UsuarioRegistrado.PermisosEspeciales
                //    .Select(pe => new PermisoEspecialUsuarioDto { 
                //        Permiso = pe.Permiso, 
                //        status = pe.status,
                //        fecEmision = pe.fecEmision,
                //        fecVencimiento = pe.fecVencimiento
                //    })
                //    .ToList()
                //},
                UsuarioRegistrado = _mapper.Map<UsuarioRegistradoDTO>(clienteExistente.UsuarioRegistrado),
            };
        }
    }
}
