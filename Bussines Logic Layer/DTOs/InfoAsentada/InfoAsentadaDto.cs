using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.DTOs.InfoAsentada
{
    public class InfoAsentadaDto
    {
        public int idInfo { get; set; }
        public DateTime fec { get; set; }
        public string Contenido { get; set; } = null!;
        public int idAlquiler { get; set; }
        public string DNIEmpleadoAsento { get; set; }
    }
}
