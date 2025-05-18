using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class InfoAsentada
    {
        public int idInfo { get; set; }
        public int idAlquiler { get; set; }
        public DateTime fec { get; set; }
        public string Contenido { get; set; } = null!;
        public Alquiler Alquiler { get; set; }
        public Empleado Empleado { get; set; }
        //Tiene lista de archivos con patron polimorfico.
    }
}
