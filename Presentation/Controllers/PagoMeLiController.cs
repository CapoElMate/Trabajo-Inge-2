using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Bussines_Logic_Layer.Managers;
using Humanizer;
using MercadoPago.Http;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PagoMeLiController : Controller
    {
        IReservaService _reservaService;

        public PagoMeLiController(IReservaService service)
        {
            _reservaService = service;
        }




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
        public async Task<IActionResult> getWebhookConfirmacion([FromBody] JsonElement jsonData)
        {
            HttpClient httpClient = new HttpClient();
            

            var result = JsonConvert.DeserializeObject<JToken>(jsonData.ToString());

            dynamic confirmacion = JValue.Parse(jsonData.ToString());
            //dynamic confirmacion = JsonConvert.DeserializeObject(jsonData);

            // Imprimir parametros como JSON
            Console.WriteLine("Webhook de confirmacion recibido: ");
            Console.WriteLine("---");
            
            Console.WriteLine(jsonData);


            Console.WriteLine("--- \n");
            Console.WriteLine("action " + result["action"]);
            Console.WriteLine("id " + result["id"]);
            Console.WriteLine("type " + result["type"]);
            Console.WriteLine("userId " + result["user_id"]);
            Console.WriteLine("dataId " + result["data"]["id"]);
            Console.WriteLine("---");


            // confirmar objeto


            var accessToken = "APP_USR-4881163094293484-052818-fc4523c66bcc46bbe3fe8b914c3dea29-2462257991";
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var paymentsApiBaseUrl = "https://api.mercadopago.com/v1/payments/";
            var paymentID = result["data"]["id"];


            string requestUrl = $"{paymentsApiBaseUrl}{paymentID}?access_token={accessToken}";
            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);


            Console.WriteLine("---");
            Console.WriteLine("response: " + response.ToString());
            Console.WriteLine("---");

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var paymentDetails = System.Text.Json.JsonSerializer.Deserialize<Root>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Esto permite que las propiedades C# coincidan con JSON sin importar mayúsculas/minúsculas
            });

            var id = Convert.ToInt32(paymentDetails.additional_info.items.First().id);

            Console.WriteLine("rta: " + id);

            _reservaService.UpdatePayment(id,Convert.ToInt32(result["data"]["id"]));


            Console.WriteLine("-ok-");
            Console.WriteLine("---");
            Console.WriteLine("---");
            Console.WriteLine("---");


            return Ok("Webhook de confirmacion recibido correctamente.");

        }

    }




    public class Accounts
    {
        public string from { get; set; }
        public string to { get; set; }
    }

    public class AdditionalInfo
    {
        public string ip_address { get; set; }
        public List<Item> items { get; set; }
        public string tracking_id { get; set; }
    }

    public class Amounts
    {
        public double original { get; set; }
        public int refunded { get; set; }
    }

    public class ApplicationData
    {
        public string name { get; set; }
        public object operating_system { get; set; }
        public string version { get; set; }
    }

    public class BusinessInfo
    {
        public string branch { get; set; }
        public string sub_unit { get; set; }
        public string unit { get; set; }
    }

    public class Card
    {
        public string bin { get; set; }
        public Cardholder cardholder { get; set; }
        public object country { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_last_updated { get; set; }
        public int expiration_month { get; set; }
        public int expiration_year { get; set; }
        public string first_six_digits { get; set; }
        public string id { get; set; }
        public string last_four_digits { get; set; }
        public List<object> tags { get; set; }
    }

    public class Cardholder
    {
        public Identification identification { get; set; }
        public string name { get; set; }
    }

    public class ChargesDetail
    {
        public Accounts accounts { get; set; }
        public Amounts amounts { get; set; }
        public int client_id { get; set; }
        public DateTime date_created { get; set; }
        public string id { get; set; }
        public DateTime last_updated { get; set; }
        public Metadata metadata { get; set; }
        public string name { get; set; }
        public List<object> refund_charges { get; set; }
        public object reserve_id { get; set; }
        public string type { get; set; }
    }

    public class ChargesExecutionInfo
    {
        public InternalExecution internal_execution { get; set; }
    }

    public class Data
    {
        public RoutingData routing_data { get; set; }
    }

    public class FeeDetail
    {
        public double amount { get; set; }
        public string fee_payer { get; set; }
        public string type { get; set; }
    }

    public class Identification
    {
        public string number { get; set; }
        public string type { get; set; }
    }

    public class InternalExecution
    {
        public DateTime date { get; set; }
        public string execution_id { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string quantity { get; set; }
        public string title { get; set; }
        public string unit_price { get; set; }
    }

    public class Metadata
    {
        public string reason { get; set; }
        public string source { get; set; }
    }

    public class Order
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Payer
    {
        public string email { get; set; }
        public object entity_type { get; set; }
        public object first_name { get; set; }
        public string id { get; set; }
        public Identification identification { get; set; }
        public object last_name { get; set; }
        public object operator_id { get; set; }
        public Phone phone { get; set; }
        public object type { get; set; }
    }

    public class PaymentMethod
    {
        public Data data { get; set; }
        public string id { get; set; }
        public string issuer_id { get; set; }
        public string type { get; set; }
    }

    public class Phone
    {
        public object number { get; set; }
        public object extension { get; set; }
        public object area_code { get; set; }
    }

    public class PointOfInteraction
    {
        public ApplicationData application_data { get; set; }
        public BusinessInfo business_info { get; set; }
        public TransactionData transaction_data { get; set; }
        public string type { get; set; }
    }

    public class Root
    {
        public object accounts_info { get; set; }
        public List<object> acquirer_reconciliation { get; set; }
        public AdditionalInfo additional_info { get; set; }
        public string authorization_code { get; set; }
        public bool binary_mode { get; set; }
        public object brand_id { get; set; }
        public string build_version { get; set; }
        public object call_for_authorize_id { get; set; }
        public bool captured { get; set; }
        public Card card { get; set; }
        public List<ChargesDetail> charges_details { get; set; }
        public ChargesExecutionInfo charges_execution_info { get; set; }
        public long collector_id { get; set; }
        public object corporation_id { get; set; }
        public object counter_currency { get; set; }
        public int coupon_amount { get; set; }
        public string currency_id { get; set; }
        public DateTime date_approved { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_last_updated { get; set; }
        public object date_of_expiration { get; set; }
        public object deduction_schema { get; set; }
        public string description { get; set; }
        public object differential_pricing_id { get; set; }
        public object external_reference { get; set; }
        public List<FeeDetail> fee_details { get; set; }
        public object financing_group { get; set; }
        public long id { get; set; }
        public int installments { get; set; }
        public object integrator_id { get; set; }
        public string issuer_id { get; set; }
        public bool live_mode { get; set; }
        public object marketplace_owner { get; set; }
        public object merchant_account_id { get; set; }
        public object merchant_number { get; set; }
        public Metadata metadata { get; set; }
        public DateTime money_release_date { get; set; }
        public object money_release_schema { get; set; }
        public string money_release_status { get; set; }
        public object notification_url { get; set; }
        public string operation_type { get; set; }
        public Order order { get; set; }
        public Payer payer { get; set; }
        public PaymentMethod payment_method { get; set; }
        public string payment_method_id { get; set; }
        public string payment_type_id { get; set; }
        public object platform_id { get; set; }
        public PointOfInteraction point_of_interaction { get; set; }
        public object pos_id { get; set; }
        public string processing_mode { get; set; }
        public List<object> refunds { get; set; }
        public object release_info { get; set; }
        public int shipping_amount { get; set; }
        public object sponsor_id { get; set; }
        public string statement_descriptor { get; set; }
        public string status { get; set; }
        public string status_detail { get; set; }
        public object store_id { get; set; }
        public object tags { get; set; }
        public int taxes_amount { get; set; }
        public int transaction_amount { get; set; }
        public int transaction_amount_refunded { get; set; }
        public TransactionDetails transaction_details { get; set; }
    }

    public class RoutingData
    {
        public string merchant_account_id { get; set; }
    }

    public class TransactionData
    {
        public object e2e_id { get; set; }
    }

    public class TransactionDetails
    {
        public object acquirer_reference { get; set; }
        public object external_resource_url { get; set; }
        public object financial_institution { get; set; }
        public int installment_amount { get; set; }
        public double net_received_amount { get; set; }
        public int overpaid_amount { get; set; }
        public object payable_deferral_period { get; set; }
        public object payment_method_reference_id { get; set; }
        public int total_paid_amount { get; set; }
    }


}