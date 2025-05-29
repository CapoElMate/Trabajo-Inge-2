using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Managers;
using Humanizer;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

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

        /*
     {
        "action":"payment.updated",
        "api_version":"v1",
        "data":{"id":"123457"},
        "date_created":"2021-11-01T02:02:02Z",
        "id":"123456",
        "live_mode":false,
        "type":"payment",
        "user_id":2462257991
      }
         */

        [HttpPost("getConfirmacionWebhook")]
        public IActionResult getWebhookConfirmacion([FromBody] string jsonData)
        {

            dynamic confirmacion = JsonConvert.DeserializeObject(jsonData);

            // Imprimir parametros como JSON
            Console.WriteLine("Webhook de confirmacion recibido: ");
            Console.WriteLine("action " + confirmacion.action);
            Console.WriteLine("id " + confirmacion.id);
            Console.WriteLine("---");


            return Ok("Webhook de confirmacion recibido correctamente.");
        }

        [HttpPost("init2")]
        public IActionResult InitializeAction([FromBody] dynamic jsonData)
        {
            return this.Ok((jsonData));
        }

    }
}
