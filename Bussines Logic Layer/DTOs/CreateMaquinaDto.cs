using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs
{
    public class CreateMaquinaDto
    {
        public string Status { get; set; } = null!;
        public int AnioFabricacion { get; set; }
        public MarcaDto Marca { get; set; } = null!;
        public ICollection<TagMaquinaDto> TagsMaquina { get; set; } = new List<TagMaquinaDto>();
        public TipoMaquinaDto TipoMaquina { get; set; } = null!;
        public ICollection<PermisoEspecialDto> PermisosEspeciales { get; set; } = new List<PermisoEspecialDto>();
    }
}
