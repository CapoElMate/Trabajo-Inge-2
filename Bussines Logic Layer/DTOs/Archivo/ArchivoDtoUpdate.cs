using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bussines_Logic_Layer.DTOs.Archivo
{
    public class ArchivoDtoUpdate
    {
        public int IdArchivo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public IFormFile? Archivo { get; set; } // opcional: puede venir para reemplazar el archivo físico
    }
}
