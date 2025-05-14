using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IUsuarioRegistrado_PermisoEspecialRepository
    {
        public UsuarioRegistrado_PermisoEspecial get(string dni, string permiso);
        public void update(UsuarioRegistrado_PermisoEspecial usuarioRegistrado_permisoEspecial);
        public void delete(string dni, string permiso);
        public void create(UsuarioRegistrado_PermisoEspecial usuarioRegistrado);
    }
}
