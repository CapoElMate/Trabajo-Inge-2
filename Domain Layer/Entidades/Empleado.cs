using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class Empleado
    {
        [Key, ForeignKey(nameof(Cliente))]
        public required string DNI { get; set; }
        public required int nroEmpleado { get; set; }
        public required Cliente Cliente { get; set; }
    }
}
