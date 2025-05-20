using Bussines_Logic_Layer.DTOs;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.DTOs
{
    public class MaquinaDto
    {
        public int IdMaquina { get; set; }
        public string Status { get; set; } = null!;
        public int AnioFabricacion { get; set; }
        public ModeloDto Modelo { get; set; } = null!;
        public ICollection<TagMaquinaDto> TagsMaquina { get; set; } = new List<TagMaquinaDto>();
        public TipoMaquinaDto TipoMaquina { get; set; } = null!;
        public ICollection<PermisoEspecialDto> PermisosEspeciales { get; set; } = new List<PermisoEspecialDto>();
    }
}
