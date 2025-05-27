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
    public class ReservaDto
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
        public TipoEntregaDto TipoEntrega { get; set; } = null!;        
        public PagoDto Pago { get; set; } = null!;        
        public AlquilerDto? Alquiler { get; set; }        
        public Bussines_Logic_Layer.DTOs.Usuarios.ClienteDto Cliente { get; set; } = null!;
        public PublicacionDto Publicacion { get; set; } = null!;
    }
}
