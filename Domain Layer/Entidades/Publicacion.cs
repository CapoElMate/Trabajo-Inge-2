using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Publicacion
    {
        [Key]
        public int idPublicacion { get; set; }
        public string Status { get; set; } = null!;
        public double PrecioPorDia { get; set; }
        public string Descripcion { get; set; } = null!;
        public Maquina Maquina { get; set; } = null!;
        public ICollection<TagPublicacion> TagsPublicacion { get; set; } = new List<TagPublicacion>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public PoliticaDeCancelacion PoliticaDeCancelacion { get; set; } = null!;
        public Ubicacion Ubicacion { get; set; } = null!;
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();
    }
}
