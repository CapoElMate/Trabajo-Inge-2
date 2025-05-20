using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Ubicacion
    {
        [Key]
        public string UbicacionName { get; set; }
        public ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
        public ICollection<Devolucion> Devoluciones { get; set; } = new List<Devolucion>();
    }
}
