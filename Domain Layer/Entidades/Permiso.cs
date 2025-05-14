using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Permiso
    {
        [Key]
        public int idPermiso { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public ICollection<Rol> Roles { get; set; } = new List<Rol>();
    }
}
