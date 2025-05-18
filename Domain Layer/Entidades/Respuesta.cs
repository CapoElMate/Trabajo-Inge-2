using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Respuesta
    {
        [Key]
        public int idRespuesta { get; set; }
        public DateTime fec { get; set; }
        public string Contenido { get; set; } = null!;
        public bool isDeleted { get; set; }
        public string DNIEmpleado { get; set; }
        public Empleado Empleado { get; set; } = null!;
        public Comentario Comentario { get; set; } = null!;
    }
}
