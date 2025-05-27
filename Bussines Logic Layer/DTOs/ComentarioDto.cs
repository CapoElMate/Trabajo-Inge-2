using Bussines_Logic_Layer.DTOs.Usuarios;

namespace Bussines_Logic_Layer.DTOs
{   
    public class ComentarioDto
    {
        public int idComentario { get; set; }
        public DateTime fec { get; set; }
        public bool isDeleted { get; set; }
        public string Contenido { get; set; } = null!;
        public ClienteDto Cliente { get; set; } = null!;
    }    
}
