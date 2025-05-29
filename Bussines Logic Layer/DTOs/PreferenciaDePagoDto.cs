using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.DTOs
{
    public class PreferenciaDePagoDto
    
    {
        //parametros para generar una preferencia de pago
        [DefaultValue("nombreProducto")]
        public string titulo { get; set; }
        [DefaultValue(1)]
        public int cantidad { get; set; }
        [DefaultValue(9)]
        public decimal precio { get; set; }
        
        
        [DefaultValue("https://localhost:5173/success")]
        public string backUrlSuccess { get; set; }
        
        
        [DefaultValue("https://localhost:5173/failure")]
        public string backUrlFailure { get; set; }


        [DefaultValue("https://localhost:5173/pending")]
        public string backUrlPending { get; set; }

    }
}
