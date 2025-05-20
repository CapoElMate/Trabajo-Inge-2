using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{
    public class UsuarioRegistradoSQL : IUsuarioRegistradoRepository
    {
        private readonly DbContext _context;

        public UsuarioRegistradoSQL(DbContext context)
        {
            _context = context;
        }

        public UsuarioRegistrado get(string dni = "", string email = "")
        {
            var user = _context.Set<UsuarioRegistrado>().FirstOrDefault(user => user.DNI == dni || user.Email == email);
            if (user != null)
                return user;
            else
                throw new InvalidOperationException("Usuario no encontrado.");
        }

        public void update(UsuarioRegistrado usuarioRegistrado)
        {
            var existingUser = _context.Set<UsuarioRegistrado>().FirstOrDefault(user => user.DNI == usuarioRegistrado.DNI);
            if (existingUser != null)
            {
                existingUser.Email = usuarioRegistrado.Email;
                //existingUser.passwordHash = usuarioRegistrado.passwordHash;
                existingUser.isDeleted = usuarioRegistrado.isDeleted;
                existingUser.Nombre = usuarioRegistrado.Nombre;
                existingUser.Apellido = usuarioRegistrado.Apellido;
                existingUser.Edad = usuarioRegistrado.Edad;
                existingUser.Telefono = usuarioRegistrado.Telefono;
                existingUser.Calle = usuarioRegistrado.Calle;
                existingUser.Altura = usuarioRegistrado.Altura;
                existingUser.Dpto = usuarioRegistrado.Dpto;
                existingUser.EntreCalles = usuarioRegistrado.EntreCalles;
                existingUser.mailVerificado = usuarioRegistrado.mailVerificado;

                _context.SaveChanges();
            }
        }

        public void delete(string dni = "", string email = "")
        {
            var user = _context.Set<UsuarioRegistrado>().FirstOrDefault(u => u.DNI == dni || u.Email == email);
            if (user != null)
            {
                _context.Set<UsuarioRegistrado>().Remove(user);
                _context.SaveChanges();
            }
        }

        public void create(UsuarioRegistrado usuarioRegistrado)
        {
            _context.Set<UsuarioRegistrado>().Add(usuarioRegistrado);
            _context.SaveChanges();
        }
    }
}
