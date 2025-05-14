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
    public class RolSQL : IRolRepository
    {
        private readonly DbContext _context;

        public RolSQL(DbContext context)
        {
            _context = context;
        }

        public Rol get(int idRol)
        {
            var rol = _context.Set<Rol>().FirstOrDefault(r => r.idRol == idRol);
            if (rol != null)
                return rol;
            else
                throw new InvalidOperationException("Usuario no encontrado.");
        }

        public void update(Rol rol)
        {
            var existingRol = _context.Set<Rol>().FirstOrDefault(eR => eR.idRol == rol.idRol);
            if (existingRol != null)
            {
                existingRol.Nombre = rol.Nombre;
                existingRol.Permisos = rol.Permisos;

                _context.SaveChanges();
            }
        }

        public void delete(int idRol)
        {
            var rol = _context.Set<Rol>().FirstOrDefault(r => r.idRol == idRol);
            if (rol != null)
            {
                _context.Set<Rol>().Remove(rol);
                _context.SaveChanges();
            }
        }

        public void create(Rol rol)
        {
            _context.Set<Rol>().Add(rol);
            _context.SaveChanges();
        }
    }
}
