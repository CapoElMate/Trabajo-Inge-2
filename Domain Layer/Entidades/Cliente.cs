using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Cliente
    {
        [Key, ForeignKey(nameof(UsuarioRegistrado))]
        public string DNI { get; set; }
        public UsuarioRegistrado UsuarioRegistrado { get; set; }
        public Empleado Empleado { get; set; }
        public ICollection<Comentario>? Comentarios { get; set; } = new List<Comentario>();
        public ICollection<Reserva>? Reservas { get; set; } = new List<Reserva>();
        public ICollection<Alquiler>? Alquileres { get; set; } = new List<Alquiler>();
        public ICollection<Reembolso>? Reembolsos { get; set; } = new List<Reembolso>();
    }
}
