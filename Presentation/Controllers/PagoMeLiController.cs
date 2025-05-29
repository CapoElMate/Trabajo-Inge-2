using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Managers;
using Humanizer;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PagoMeLiController : Controller
    {
        [HttpPost("generarPreferenciaDePago")]
        public Task<Preference> GenerarPreferenciaDePago(PreferenciaDePagoDto parametros)
        {
            GenerarPreferenciaDePago genPref = new GenerarPreferenciaDePago();

            var preferencia = genPref.getPreferenciaAsync(parametros);
            return preferencia;
        }

        [HttpPost("getConfirmacionWebhook")]
        public IActionResult getWebhookConfirmacion(string parametros)
        {

            //imprimir parametros
            Console.WriteLine("Webhook de confirmacion recibido: " + parametros);

            return Ok("Webhook de confirmacion recibido correctamente.");
        }

    }
}
