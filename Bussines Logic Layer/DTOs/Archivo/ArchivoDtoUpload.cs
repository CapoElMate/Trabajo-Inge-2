﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Bussines_Logic_Layer.DTOs.Archivo
{
    public class ArchivoDtoUpload
    {
        public int EntidadID { get; set; }
        public TipoEntidadArchivo TipoEntidad { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public IFormFile Archivo { get; set; } = null!;
    }
}
