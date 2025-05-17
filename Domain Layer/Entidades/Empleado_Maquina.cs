using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Empleado_Maquina
    {
        public DateTime fecInicio { get; set; }
        public DateTime fecFin { get; set; }

        public int nroEmpleado { get; set; }
        public Empleado empleado { get; set; } = null!;
        public int idMaquina { get; set; }
        public Maquina maquina { get; set; } = null!;
    }
}
