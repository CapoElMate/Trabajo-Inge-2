using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class AlquilerDto
    {
        public int idAlquiler { get; set; }
        public bool isDeleted { get; set; }
        public DateTime fecEfectivizacion { get; set; }
        public string DNICliente { get; set; }
        public Cliente Cliente { get; set; }
        public Reembolso? Reembolso { get; set; }
        public string DNIEmpleado { get; set; }
        public Empleado Empleado { get; set; }
        public ICollection<InfoAsentada>? InfoAsentada { get; set; } = new List<InfoAsentada>();
        public Devolucion Devolucion { get; set; }
        public int idReserva { get; set; }
        public Reserva Reserva { get; set; }
        //Tiene lista de archivos con patron polimorfico.
    }
}
