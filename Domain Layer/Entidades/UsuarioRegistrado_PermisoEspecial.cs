using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class UsuarioRegistrado_PermisoEspecial
    {
        public DateTime fecEmision { get; set; }
        public DateTime fecVencimiento { get; set; }
        public string status { get; set; } = null!;

        public string UsuarioRegistradoDNI { get; set; }
        public UsuarioRegistrado UsuarioRegistrado { get; set; }

        public string Permiso { get; set; }
        public PermisoEspecial PermisoEspecial { get; set; }
    }
}
