using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Recargo
    {
        public int idRecargo { get; set; }
        public int idDevolucion { get; set; }
        public string Status { get; set; } = null!;
        public double Total { get; set; }
        public string Descripcion { get; set; }
        public Devolucion Devolucion { get; set; }
    }
}
