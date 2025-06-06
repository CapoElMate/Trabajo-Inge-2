using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Maquina
    {
        [Key]
        public int idMaquina { get; set; }
        public bool isDeleted { get; set; }
        public string status { get; set; } = null!;
        public int anioFabricacion { get; set; }
        public ICollection<Empleado_Maquina> Empleado_Maquinas { get; set; } = new List<Empleado_Maquina>();
        [ForeignKey(nameof(Modelo))]
        public string ModeloName { get; set; }
        public Modelo Modelo { get; set; } = null!;
        public ICollection<TagMaquina> TagsMaquina { get; set; } = new List<TagMaquina>();
        public string Tipo { get; set; }
        public TipoMaquina TipoMaquina { get; set; } = null!;
        public ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
        public ICollection<PermisoEspecial> PermisosEspeciales { get; set; } = new List<PermisoEspecial>();
    }
}
