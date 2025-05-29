using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercadoPago;
using Bussines_Logic_Layer.DTOs;
using Humanizer;

namespace Bussines_Logic_Layer.Managers
{
    public class GenerarPreferenciaDePago
    {

        public Task<Preference> getPreferenciaAsync(PreferenciaDePagoDto dto)
        {
            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Id = dto.idProducto,
                        Title = dto.titulo,
                        Quantity = dto.cantidad,
                        CurrencyId = "ARS",
                        UnitPrice = dto.precio,
                    },
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = dto.backUrlSuccess,
                    Failure = dto.backUrlFailure,
                    Pending = dto.backUrlPending,
                },
                AutoReturn = "approved",

            };

            // Crea la preferencia usando el client
            var client = new PreferenceClient();
            Task<Preference> preference = client.CreateAsync(request);

            return preference;
        }
    }
}
