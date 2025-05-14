using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{
    public class UsuarioRegistrado_PermisoEspecialSQL : IUsuarioRegistrado_PermisoEspecialRepository
    {
        private readonly DbContext _context;

        public UsuarioRegistrado_PermisoEspecialSQL(DbContext context)
        {
            _context = context;
        }

        public UsuarioRegistrado_PermisoEspecial get(string DNI, string Permiso)
        {
            var usuarioRegistrado_permisoEspecial = _context.Set<UsuarioRegistrado_PermisoEspecial>().FirstOrDefault(uR_pE => (uR_pE.UsuarioRegistradoDNI == DNI) && (uR_pE.Permiso == Permiso));
            if (usuarioRegistrado_permisoEspecial != null)
                return usuarioRegistrado_permisoEspecial;
            else
                throw new InvalidOperationException("Usuario no encontrado.");
        }

        public void update(UsuarioRegistrado_PermisoEspecial UsuarioRegistrado_PermisoEspecial)
        {
            var existingUsuarioRegistrado_PermisoEspecial = _context.Set<UsuarioRegistrado_PermisoEspecial>().FirstOrDefault(e_uR_pE => (e_uR_pE.UsuarioRegistradoDNI == UsuarioRegistrado_PermisoEspecial.UsuarioRegistradoDNI) && (e_uR_pE.Permiso == UsuarioRegistrado_PermisoEspecial.Permiso));
            if (existingUsuarioRegistrado_PermisoEspecial != null)
            {
                existingUsuarioRegistrado_PermisoEspecial.fecEmision = UsuarioRegistrado_PermisoEspecial.fecEmision;
                existingUsuarioRegistrado_PermisoEspecial.fecVencimiento = UsuarioRegistrado_PermisoEspecial.fecVencimiento;
                existingUsuarioRegistrado_PermisoEspecial.status = UsuarioRegistrado_PermisoEspecial.status;

                _context.SaveChanges();
            }
        }

        public void delete(string DNI, string Permiso)
        {
            var usuarioRegistrado_permisoEspecial = _context.Set<UsuarioRegistrado_PermisoEspecial>().FirstOrDefault(uR_pE => (uR_pE.UsuarioRegistradoDNI == DNI) && (uR_pE.Permiso == Permiso));
            if (usuarioRegistrado_permisoEspecial != null)
            {
                _context.Set<UsuarioRegistrado_PermisoEspecial>().Remove(usuarioRegistrado_permisoEspecial);
                _context.SaveChanges();
            }
        }

        public void create(UsuarioRegistrado_PermisoEspecial usuarioRegistrado_permisoEspecial)
        {
            _context.Set<UsuarioRegistrado_PermisoEspecial>().Add(usuarioRegistrado_permisoEspecial);
            _context.SaveChanges();
        }
    }
}
