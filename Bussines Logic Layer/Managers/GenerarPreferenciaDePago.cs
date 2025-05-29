using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercadoPago;
using Bussines_Logic_Layer.DTOs;

namespace Bussines_Logic_Layer.Managers
{
    public class GenerarPreferenciaDePago
    {
        //para hacerns la vida mas simple , creo dos metodos que hacen lo mismo , pero pasandoles el dto.
        public Task<Preference> getPreferenciaAsync(PreferenciaDePagoDto dto)
        {
            return getIdPreferenciaAsync(dto.titulo, dto.cantidad, dto.precio,dto.backUrlSuccess,dto.backUrlFailure,dto.backUrlPending);
        }

        public Preference getPreferencia(PreferenciaDePagoDto dto)
        {
            return getPreferencia(dto.titulo, dto.cantidad, dto.precio, dto.backUrlSuccess, dto.backUrlFailure, dto.backUrlPending);
        }


        // metodos que crean la preferencia de pago (id que sirve para luego pagar), con el cliente de MercadoPago.
        public Preference getPreferencia(string titulo, int cantidad, decimal precio,string backUrlSuccess,string backUrlFailure, string backUrlPending)
        {

            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = titulo,
                        Quantity = cantidad,
                        CurrencyId = "ARS",
                        UnitPrice = precio,
                    },
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = backUrlSuccess,
                    Failure = backUrlFailure,
                    Pending = backUrlPending,
                },
                AutoReturn = "approved",
            };

            // Crea la preferencia usando el client
            var client = new PreferenceClient();
            Preference preference = client.Create(request);

            return preference;
        }


        public async Task<Preference> getIdPreferenciaAsync(string titulo, int cantidad, decimal precio, string backUrlSuccess, string backUrlFailure, string backUrlPending)
        {

            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = titulo,
                        Quantity = cantidad,
                        CurrencyId = "ARS",
                        UnitPrice = precio,
                    },
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = backUrlSuccess,
                    Failure = backUrlFailure,
                    Pending = backUrlPending,
                },
                AutoReturn = "approved",
            };

            // Crea la preferencia usando el client
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);

            return preference;
        }
    }
}
