using Bussines_Logic_Layer.DTOs.Maquina;
using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs.Publicacion
{
    public class CreatePublicacionDto
    {
        public string Status { get; set; } = null!;
        public double PrecioPorDia { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Titulo { get; set; }
        public int IdMaquina { get; set; }
        public ICollection<TagPublicacionDto> TagsPublicacion { get; set; } = new List<TagPublicacionDto>();
        //public ICollection<ComentarioDto> Comentarios { get; set; } = new List<ComentarioDto>();        
        public PoliticaDeCancelacionDto PoliticaDeCancelacion { get; set; } = null!;
        public UbicacionDto Ubicacion { get; set; } = null!;
            
    }
}
