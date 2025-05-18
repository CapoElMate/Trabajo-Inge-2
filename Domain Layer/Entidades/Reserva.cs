using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Reserva
    {
        [Key]
        public int idReserva { get; set; }
        public DateTime fecInicio { get; set; }
        public DateTime fecFin { get; set; }
        public string Status { get; set; } = null!;
        public double montoTotal { get; set; }
        public string Calle { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public string? Dpto { get; set; }
        public string EntreCalles { get; set; } = null!;
        public TipoEntrega TipoEntrega { get; set; } = null!;
        public Pago Pago { get; set; } = null!;
        public Alquiler? Alquiler { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public Publicacion Publicacion { get; set; } = null!;
    }
}
