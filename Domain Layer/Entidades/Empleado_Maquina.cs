using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Empleado_Maquina
    {
        public DateTime FecInicio { get; set; }
        public DateTime FecFin { get; set; }

        public int NroEmpleado { get; set; }
        public Empleado Empleado { get; set; } = null!;
        public int IdMaquina { get; set; }
        public Maquina Maquina { get; set; } = null!;
    }
}
