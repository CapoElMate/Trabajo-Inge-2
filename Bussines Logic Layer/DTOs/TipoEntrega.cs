using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class TipoEntrega
    {
        [Key]
        public string Entrega { get; set; } = null!;
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
