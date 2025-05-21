using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs
{
    public class ModeloDto
    {
        public string Modelo { get; set; } = null!;
        public MarcaDto Marca { get; set; } = null!;
    }
}
