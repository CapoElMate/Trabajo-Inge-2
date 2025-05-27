using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class EmpleadoDto
    {
        public string DNI { get; set; }
        public int nroEmpleado { get; set; }
        public Cliente Cliente { get; set; }
        
        public ICollection<Empleado_Maquina>? Empleado_Maquinas { get; set; } = new List<Empleado_Maquina>(); //no toco por las dudas
        //public ICollection<RespuestaDto>? Respuestas { get; set; } = new List<RespuestaDto>();
        //public ICollection<AlquilerDto>? Alquileres { get; set; } = new List<AlquilerDto>();
        //public ICollection<InfoAsentadaDto>? InfoAsentada { get; set; } = new List<InfoAsentadaDto>();
        //public ICollection<DevolucionDto> Devoluciones { get; set; } = new List<DevolucionDto>();

    }
}
