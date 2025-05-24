using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs
{
    public class PublicacionDto
    {
        public int idPublicacion { get; set; }
        public string Status { get; set; } = null!;
        public double PrecioPorDia { get; set; }
        public string Descripcion { get; set; } = null!;        
        public MaquinaDto Maquina { get; set; } = null!;
        public ICollection<TagPublicacionDto> TagsPublicacion { get; set; } = new List<TagPublicacionDto>();
        public ICollection<ComentarioDto> Comentarios { get; set; } = new List<Comentario>();        
        public PoliticaDeCancelacionDto PoliticaDeCancelacion { get; set; } = null!;
        public UbicacionDto Ubicacion { get; set; } = null!;
            
    }
}
