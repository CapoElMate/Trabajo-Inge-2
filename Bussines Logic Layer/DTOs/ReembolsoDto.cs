using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class ReembolsoDto
    {
        public int idReembolso { get; set; }
        public string Status { get; set; } = null!;
        public double Monto { get; set; }
        public string Motivo { get; set; } = null!;
        public Cliente Cliente { get; set; }
        public AlquilerDto Alquiler { get; set; }
    }
}
