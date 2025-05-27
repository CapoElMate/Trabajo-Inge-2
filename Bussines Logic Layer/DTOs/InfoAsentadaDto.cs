using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class InfoAsentadaDto
    {
        public int idInfo { get; set; }
        public DateTime fec { get; set; }
        public string Contenido { get; set; } = null!;
        public AlquilerDto Alquiler { get; set; }
        public EmpleadoDto Empleado { get; set; }
        //Tiene lista de archivos con patron polimorfico.
    }
}
