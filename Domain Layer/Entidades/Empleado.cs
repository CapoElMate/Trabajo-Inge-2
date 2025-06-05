using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Empleado
    {
        [Key, ForeignKey(nameof(Cliente))]
        public string DNI { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nroEmpleado { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<Empleado_Maquina>? Empleado_Maquinas { get; set; } = new List<Empleado_Maquina>();
        public ICollection<Respuesta>? Respuestas { get; set; } = new List<Respuesta>();
        public ICollection<Alquiler>? Alquileres { get; set; } = new List<Alquiler>();
        public ICollection<InfoAsentada>? InfoAsentada { get; set; } = new List<InfoAsentada>();
        public ICollection<Devolucion> Devoluciones { get; set; } = new List<Devolucion>();

    }
}
