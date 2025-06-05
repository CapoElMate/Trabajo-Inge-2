using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs
{
    public class ReservasDto
    {
        public int idReserva { get; set; }
        public DateTime fecInicio { get; set; }
        public DateTime fecFin { get; set; }
        public string Status { get; set; } = null!;
        public double montoTotal { get; set; }
        public string Calle { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public string? Dpto { get; set; }
        public string EntreCalles { get; set; } = null!;
        public string Entrega { get; set; } = null!;
        public TipoEntrega TipoEntrega { get; set; } = null!;
        public int nroPago { get; set; }
        public Pago Pago { get; set; } = null!;
        public int idAlquiler { get; set; }
        public Domain_Layer.Entidades.Alquiler? Alquiler { get; set; }
        public string DNI { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public int idPublicacion { get; set; }
        public Domain_Layer.Entidades.Publicacion Publicacion { get; set; } = null!;
    }
}
