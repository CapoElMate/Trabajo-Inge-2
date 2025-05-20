using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class PermisoEspecial
    {
        public string Permiso { get; set; } = null!;
        public ICollection<UsuarioRegistrado_PermisoEspecial> UsuariosRegistrados { get; set; } = new List<UsuarioRegistrado_PermisoEspecial>();
        public ICollection<Maquina> Maquinaria { get; set; } = new List<Maquina>();
    }
}
