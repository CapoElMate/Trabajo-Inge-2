using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.ValueObjects;

namespace Domain_Layer.Entidades
{
    public class Archivo
    {
        public int idArchivo { get; set; }
        public int EntidadID { get; set; }
        public TipoEntidadArchivo TipoEntidad { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string TipoContenido { get; set; } = null!;
        public string Ruta { get; set; } = null!;
    }
}
