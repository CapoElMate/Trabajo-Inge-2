using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{


    public class UsuarioRegistrado
    {

        public UsuarioRegistrado()
        {
        }

        public string Email { get; set; } = null!;
        [StringLength(8, MinimumLength = 6)]
        public string DNI { get; set; } = null!;
        public bool isDeleted { get; set; }
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
        public bool mailVerificado { get; set; }
        public ICollection<UsuarioRegistrado_PermisoEspecial> PermisosEspeciales { get; set; } = new List<UsuarioRegistrado_PermisoEspecial>();
        public Cliente? Cliente { get; set; } = null!;
    }
}
