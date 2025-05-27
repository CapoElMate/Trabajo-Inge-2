using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Usuarios;
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
        public ClienteDto Cliente { get; set; }
        //public ReembolsoDto? Reembolso { get; set; }
        public EmpleadoDto Empleado { get; set; }
        //public ICollection<InfoAsentadaDto>? InfoAsentada { get; set; } = new List<InfoAsentadaDto>();
        public DevolucionDto Devolucion { get; set; }
        
        //public ReservaDto Reserva { get; set; }
        //Tiene lista de archivos con patron polimorfico.
    }
}
