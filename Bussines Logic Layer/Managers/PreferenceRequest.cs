using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercadoPago;
/*
namespace Bussines_Logic_Layer.Managers
{
    public class PreferenceRequest
    {
        public string createReference()
        {

            // Crea el objeto de request de la preference
            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = "Mi producto",
                        Quantity = 1,
                        CurrencyId = "ARS",
                        UnitPrice = 75.56m,
                    },
                },
            };

            // Crea la preferencia usando el client
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
        }


    }
}
*/