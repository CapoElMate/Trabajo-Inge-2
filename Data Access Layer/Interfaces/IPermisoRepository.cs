using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IPermisoRepository
    {
        public Permiso get(int idPermiso);
        public void update(Permiso permiso);
        public void delete(int idPermiso);
        public void create(Permiso permiso);
    }
}
