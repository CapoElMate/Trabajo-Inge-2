using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Modelo
    {
        [Key]
        public string ModeloName { get; set; } = null!;
        public string MarcaName { get; set; } = null!;
        public Marca Marca { get; set; } = null!;
    }
}
