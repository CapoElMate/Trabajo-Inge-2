using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs
{
    public class ClienteDto
    {
        public string DNI { get; set; }
        public UsuarioRegistrado UsuarioRegistrado { get; set; }
                
    }
}
