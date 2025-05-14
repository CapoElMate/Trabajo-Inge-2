using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Cliente
    {
        [Key, ForeignKey(nameof(UsuarioRegistrado))]
        public required string DNI { get; set; }
        public required UsuarioRegistrado UsuarioRegistrado { get; set; }
        public Empleado Empleado { get; set; } = null!;
    }
}
