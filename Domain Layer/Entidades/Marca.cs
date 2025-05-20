using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Marca
    {
        [Key]
        public string MarcaName { get; set; }
        public ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
    }
}
