using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IEmpleadoRepository
    {
        public Empleado get(string dni = "", string email = "", int nroEmpleado = -1);
        public void update(Empleado empleado);
        public void delete(string dni = "", string email = "", int nroEmpleado = -1);
        public void create(Empleado empleado);
    }
}
