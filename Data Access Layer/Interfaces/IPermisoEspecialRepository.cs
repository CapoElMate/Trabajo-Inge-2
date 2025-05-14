using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IPermisoEspecialRepository
    {
        public PermisoEspecial get(string permiso);
        public void update(PermisoEspecial permisoEspecial);
        public void delete(string permiso);
        public void create(PermisoEspecial permisoEspecial);
    }
}
