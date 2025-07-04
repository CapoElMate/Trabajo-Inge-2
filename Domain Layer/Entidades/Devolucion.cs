﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Devolucion
    {
        public int idDevolucion { get; set; }
        public DateTime fecDevolucion { get; set; }
        public string Descripcion { get; set; } = null!;
        public ICollection<Recargo> Recargos { get; set; } = new List<Recargo>();
        public string DNIEmpleado { get; set; }
        public Empleado Empleado { get; set; }
        public string UbicacionName { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public int idAlquiler { get; set; }
        public Alquiler Alquiler { get; set; }
    }
}
