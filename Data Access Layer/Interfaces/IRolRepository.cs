using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IRolRepository
    {
        public Rol get(int idRol);
        public void update(Rol rol);
        public void delete(int idRol);
        public void create(Rol rol);
    }
}
