using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.InfoAsentada;
using Bussines_Logic_Layer.DTOs.Reserva;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.DTOs
{
    public class CreateAlquilerDto
    {
        public DateTime fecEfectivizacion { get; set; }
        public string status { get; set; }
        public string DNICliente { get; set; }
        public string DNIEmpleadoEfectivizo { get; set; }
        public ICollection<InfoAsentadaDto>? InfoAsentada { get; set; } = new List<InfoAsentadaDto>();
        public ReservaDto Reserva { get; set; }
    }
}
