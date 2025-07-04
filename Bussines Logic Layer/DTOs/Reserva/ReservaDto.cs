﻿using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Bussines_Logic_Layer.DTOs.Publicacion;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs.Reserva
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
        public string Piso { get; set; } = null!;
        public TipoEntregaDto TipoEntrega { get; set; } = null!;        
        //public PagoDto Pago { get; set; } = null!;
        public int? IdAlquiler { get; set; }
        public string DNICliente { get; set; }
        public int IdPublicacion { get; set; } 
        //public AlquilerDto? Alquiler { get; set; }        
        //public ClienteDto Cliente { get; set; } = null!;
        //public PublicacionDto Publicacion { get; set; } = null!;
    }
}
