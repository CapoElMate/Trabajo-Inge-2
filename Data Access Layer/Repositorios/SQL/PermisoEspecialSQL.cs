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
    public class PermisoEspecialSQL : IPermisoEspecialRepository
    {
        private readonly DbContext _context;

        public PermisoEspecialSQL(DbContext context)
        {
            _context = context;
        }

        public PermisoEspecial get(string permiso)
        {
            var permisoEspecial = _context.Set<PermisoEspecial>().FirstOrDefault(pE => pE.Permiso == permiso);
            if (permisoEspecial != null)
                return permisoEspecial;
            else
                throw new InvalidOperationException("Usuario no encontrado.");
        }

        public void update(PermisoEspecial permisoEspecial)
        {
            var existingPermisoEspecial = _context.Set<PermisoEspecial>().FirstOrDefault(pE => pE.Permiso == permisoEspecial.Permiso);
            if (existingPermisoEspecial != null)
            {
                //existingPermisoEspecial.Maquinaria = permisoEspecial.Maquinaria;
                existingPermisoEspecial.UsuariosRegistrados = permisoEspecial.UsuariosRegistrados;

                _context.SaveChanges();
            }
        }

        public void delete(string permiso)
        {
            var permisoEspecial = _context.Set<PermisoEspecial>().FirstOrDefault(pE => pE.Permiso == permiso);
            if (permisoEspecial != null)
            {
                _context.Set<PermisoEspecial>().Remove(permisoEspecial);
                _context.SaveChanges();
            }
        }

        public void create(PermisoEspecial permisoEspecial)
        {
            _context.Set<PermisoEspecial>().Add(permisoEspecial);
            _context.SaveChanges();
        }
    }
}
