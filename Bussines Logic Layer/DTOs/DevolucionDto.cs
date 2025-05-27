using Bussines_Logic_Layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entidades
{
    public class DevolucionDto
    {
        public int idDevolucion { get; set; }
        public DateTime fecDevolucion { get; set; }
        public string Descripcion { get; set; } = null!;
        public ICollection<RecargoDto> Recargos { get; set; } = new List<RecargoDto>();
        public EmpleadoDto Empleado { get; set; }
        public UbicacionDto Ubicacion { get; set; }
        //public AlquilerDto Alquiler { get; set; }
    }
}
