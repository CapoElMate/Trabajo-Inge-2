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
        public bool isDeleted { get; set; }
        public string Status { get; set; } = null!;
        public double PrecioPorDia { get; set; }
        public string Descripcion { get; set; } = null!;
        public int idMaquina { get; set; }
        public Maquina Maquina { get; set; } = null!;
        public ICollection<TagPublicacion> TagsPublicacion { get; set; } = new List<TagPublicacion>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public string Politica { get; set; } = null!;
        public PoliticaDeCancelacion PoliticaDeCancelacion { get; set; } = null!;
        public string UbicacionName { get; set; }
        public Ubicacion Ubicacion { get; set; } = null!;
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        //Tiene lista de archivos con patron polimorfico.
    }
}
