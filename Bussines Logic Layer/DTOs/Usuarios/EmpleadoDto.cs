using System;
using System.Collections.Generic;

namespace Bussines_Logic_Layer.DTOs.Usuarios
{
    public class EmpleadoDTO
    {
        public int nroEmpleado { get; set; }
        public ClienteDTO Cliente { get; set; }
    }
}
