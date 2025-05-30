using Data_Access_Layer.Configurations;
using Domain_Layer.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Bussines_Logic_Layer.DTOs.Usuarios
{
    public class UsuarioRegistradoDTO
    {
        public string Email { get; set; } = null!;
        [StringLength(8, MinimumLength = 6)]
        public string DNI { get; set; } = null!;
        [StringLength(50, MinimumLength = 2)]
        public string Nombre { get; set; } = null!;
        [StringLength(50, MinimumLength = 2)]
        public string Apellido { get; set; } = null!;
        [Range(18, 120)]
        public int Edad { get; set; }
        public string Telefono { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public string? Dpto { get; set; }
        public string EntreCalles { get; set; } = null!;
        public ICollection<PermisoEspecialUsuarioDto> PermisosEspeciales { get; set; } = new List<PermisoEspecialUsuarioDto>();
        public string roleName { get; set; } = null!;
        public bool dniVerificado { get; set; }
    }
}
