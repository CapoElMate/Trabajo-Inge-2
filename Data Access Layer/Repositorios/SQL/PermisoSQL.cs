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
    public class PermisoSQL : IPermisoRepository
    {
        private readonly DbContext _context;

        public PermisoSQL(DbContext context)
        {
            _context = context;
        }

        public Permiso get(int idPermiso)
        {
            var permiso = _context.Set<Permiso>().FirstOrDefault(p => p.idPermiso == idPermiso);
            if (permiso != null)
                return permiso;
            else
                throw new InvalidOperationException("Usuario no encontrado.");
        }

        public void update(Permiso permiso)
        {
            var existingPermiso = _context.Set<Permiso>().FirstOrDefault(existingP => existingP.idPermiso == permiso.idPermiso);
            if (existingPermiso != null)
            {
                existingPermiso.Nombre = permiso.Nombre;
                existingPermiso.Descripcion = permiso.Descripcion;
                existingPermiso.Roles = permiso.Roles;

                _context.SaveChanges();
            }
        }

        public void delete(int idPermiso)
        {
            var permiso = _context.Set<Permiso>().FirstOrDefault(p => p.idPermiso == idPermiso);
            if (permiso != null)
            {
                _context.Set<Permiso>().Remove(permiso);
                _context.SaveChanges();
            }
        }

        public void create(Permiso permiso)
        {
            _context.Set<Permiso>().Add(permiso);
            _context.SaveChanges();
        }
    }
}
