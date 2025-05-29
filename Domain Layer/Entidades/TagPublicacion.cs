using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class TagPublicacion
    {
        [Key]
        public string Tag { get; set; } = null!;
        public ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
    }
}
