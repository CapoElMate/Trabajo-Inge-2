using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class UsuarioRegistrado
    {
        public string Email { get; set; } = null!;
        public string DNI { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
        public bool isDeleted { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Edad { get; set; }
        public string Telefono { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public string? Dpto { get; set; }
        public string EntreCalles { get; set; } = null!;
        public bool mailVerificado { get; set; }
    }
}
