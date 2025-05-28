using System;
using System.Collections.Generic;

namespace Bussines_Logic_Layer.DTOs.Usuarios
{
    public class EmpleadoDto
    {
        public int nroEmpleado { get; set; }
        public ClienteDto Cliente { get; set; }
    }
}
