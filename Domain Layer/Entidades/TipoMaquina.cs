using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class TipoMaquina
    {
        [Key]
        public string Tipo { get; set; }
        public ICollection<Maquina> Maquinas { get; set; } = new List<Maquina>();
    }
}
