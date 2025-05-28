using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.InfoAsentada;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.DTOs.Alquiler
{
    public class AlquilerDto
    {
        public int idAlquiler { get; set; }
        public DateTime fecEfectivizacion { get; set; }
        public string DNICliente { get; set; }
        public string DNIEmpleadoEfectivizo { get; set; }
        public ICollection<InfoAsentadaDto>? InfoAsentada { get; set; } = new List<InfoAsentadaDto>();
        public int idReserva { get; set; }
    }
}
