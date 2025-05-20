using Data_Access_Layer.Configurations;
using Domain_Layer.Entidades;
using System.ComponentModel.DataAnnotations;

namespace API_Layer.DTOs
{
    public class RegistroUsuarioCompletoDTO
    {

        public RegistroUsuarioCompletoDTO(){}

        public UsuarioRegistrado getUsuarioRegistrado() //esta funcion devuelve el tipo de dato de usuario registrado
        {
            UsuarioRegistrado u = new UsuarioRegistrado();

            u.DNI = DNI;
            u.Email = Email;
            u.isDeleted = isDeleted;
            u.Nombre = Nombre;
            u.Apellido = Apellido;
            u.Edad = Edad;
            u.Telefono = Telefono;
            u.Calle = Calle;
            u.Altura = Altura;
            u.Dpto = Dpto;
            u.EntreCalles = EntreCalles;
            u.mailVerificado = mailVerificado;
            u.PermisosEspeciales = PermisosEspeciales;
            u.Cliente = Cliente;

            return u;
        }

        //todos los datos de IdentityUser
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        //todos los datos de UsuarioRegistrado

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
