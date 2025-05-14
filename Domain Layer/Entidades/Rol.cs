using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Rol
    {
        [Key]
        public int idRol { get; set; }
        public string Nombre { get; set; } = null!;
        public ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
    }
}
