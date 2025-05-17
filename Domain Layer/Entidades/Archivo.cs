using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Archivo
    {
        public int idArchivo { get; set; }
        public int EntidadID { get; set; }
        public string TipoEntidad { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string TipoContenido { get; set; } = null!;
        public string Ruta { get; set; } = null!;
    }
}
