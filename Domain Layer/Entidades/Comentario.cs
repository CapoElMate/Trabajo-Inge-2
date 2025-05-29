using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Comentario
    {
        public int idComentario { get; set; }
        public DateTime fec { get; set; }
        public bool isDeleted { get; set; }
        public string Contenido { get; set; } = null!;
        public int idRespuesta { get; set; }
        public Respuesta Respuesta { get; set; } = null!;
        public int idPublicacion { get; set; }
        public Publicacion Publicacion { get; set; } = null!;
        public string DNICliente { get; set; }
        public Cliente Cliente { get; set; } = null!;

    }
}
