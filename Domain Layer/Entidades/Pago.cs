using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Pago
    {
        public long nroPago { get; set; }
        public DateTime fecPago { get; set; }
        public int idReserva { get; set; }
        public Reserva Reserva { get; set; } = null!;
    }
}
