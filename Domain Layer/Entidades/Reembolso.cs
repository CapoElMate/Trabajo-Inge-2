using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Reembolso
    {
        public int idReembolso { get; set; }
        public string Status { get; set; } = null!;
        public double Monto { get; set; }
        public string Motivo { get; set; } = null!;
        public string DNICliente { get; set; }
        public Cliente Cliente { get; set; }
        public int idAlquiler { get; set; }
        public Alquiler Alquiler { get; set; }
    }
}
