using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cotizacion.ExternalServices
{
    public class BancoProvinciaService : IBancoProvinciaService
    {
        public HttpClient Client { get; }

        public BancoProvinciaService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.bancoprovincia.com.ar/Principal/Dolar");
            Client = client;
        }

        public async Task<List<string>> GetDolar()
        {
            var response = await Client.GetAsync("");
            response.EnsureSuccessStatusCode();
            var result = await response.Content
                .ReadAsAsync<List<string>>();
            return result;
        }
    }
}
