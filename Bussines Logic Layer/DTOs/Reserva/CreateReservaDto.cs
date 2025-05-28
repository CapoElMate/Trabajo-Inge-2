using Bussines_Logic_Layer.DTOs.Publicacion;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs.Reserva
{
    public class CreateReservaDto
    {
        public DateTime fecInicio { get; set; }
        public DateTime fecFin { get; set; }
        public string Status { get; set; } = null!;
        public double montoTotal { get; set; }
        public string Calle { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public string? Dpto { get; set; }
        public string EntreCalles { get; set; } = null!;
        public TipoEntregaDto TipoEntrega { get; set; } = null!;
        public PagoDto Pago { get; set; } = null!;
        public AlquilerDto? Alquiler { get; set; }
        public Usuarios.ClienteDto Cliente { get; set; } = null!;
        public PublicacionDto Publicacion { get; set; } = null!;
    }
}
