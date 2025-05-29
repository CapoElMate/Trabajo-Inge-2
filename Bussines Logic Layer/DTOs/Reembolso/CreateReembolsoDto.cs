using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs.Reembolso
{
    public class CreateReembolsoDto
    {
        public int idReembolso { get; set; }
        public string Status { get; set; } = null!;
        public double Monto { get; set; }
        public string Motivo { get; set; } = null!;
        public string DNICliente { get; set; }
        public int idAlquiler { get; set; }
    }
}
