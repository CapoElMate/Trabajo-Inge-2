using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Alquiler
    {
        [Key]
        public int idAlquiler { get; set; }
        public bool isDeleted { get; set; }
        public DateTime fecEfectivizacion { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public Reembolso? Reembolso { get; set; }
        public Empleado Empleado { get; set; } = null!;
        public ICollection<InfoAsentada>? InfoAsentada { get; set; } = new List<InfoAsentada>();
        public Devolucion Devolucion { get; set; } = null!;
        public Reserva Reserva { get; set; } = null!;
        //Tiene lista de archivos con patron polimorfico.
    }
}
