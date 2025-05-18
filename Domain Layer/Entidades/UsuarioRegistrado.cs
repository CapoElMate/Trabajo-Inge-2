using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class UsuarioRegistrado
    {

        public UsuarioRegistrado(string email, string dNI, string passwordHash, bool isDeleted, string nombre, string apellido, int edad, string telefono, string calle, string altura, string? dpto, string entreCalles, bool mailVerificado)
        {
            Email = email;
            DNI = dNI;
            this.passwordHash = passwordHash;
            this.isDeleted = isDeleted;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Telefono = telefono;
            Calle = calle;
            Altura = altura;
            Dpto = dpto;
            EntreCalles = entreCalles;
            this.mailVerificado = mailVerificado;
        }

        public string Email { get; set; } = null!;
        [StringLength(8, MinimumLength = 6)]
        public string DNI { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
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
        public Cliente Cliente { get; set; } = null!;
    }
}
