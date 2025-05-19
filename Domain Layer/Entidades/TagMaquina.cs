using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class TagMaquina
    {
        [Key]
        public string Tag { get; set; } = null!;
        public ICollection<Maquina> Maquinas { get; set; } = new List<Maquina>();
    }
}
