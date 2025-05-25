using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.DTOs.Usuarios
{
    public class PermisoEspecialUsuarioDto
    {
        public string DNICliente { get; set; } = null!;
        public DateTime fecEmision { get; set; }
        public DateTime fecVencimiento { get; set; }
        public string status { get; set; } = null!;
        public PermisoEspecialDto Permiso { get; set; } = null!;
    }
}
