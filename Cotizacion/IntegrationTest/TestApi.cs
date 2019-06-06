using Cotizacion;
using Cotizacion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class TestApi :
         IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly HttpClient _client;

        public TestApi(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetCotizacion()
        {
            List<string> coti = new List<string>()
            {
                "44.000","46.000","Actualizada al 5/6/2019 15:00"
            };
            var httpResponse = await _client.GetAsync("/api/Cotizacion/Dolar");
            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var Cotizacion = JsonConvert.DeserializeObject<List<string>>(stringResponse);
            Assert.Equal(Cotizacion, coti);

        }

        [Fact]
        public async Task CanGetUser()
        {
            var httpResponse = await _client.GetAsync("/api/Users/");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<List<User>>(stringResponse);
            Type type = new TypeDelegator(typeof(List<User>));
            Assert.False(User.GetType() == type);
        }
        [Fact]
        public async Task CanGetUserbyId()
        {
            var httpResponse = await _client.GetAsync("/api/Users/5cf5ea1f290d1236a8eaafd9");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<User>(stringResponse);
            Type type = new TypeDelegator(typeof(User));
            Assert.False(User.GetType() == type);
        }

        [Fact]
        public async Task CanGetUsuario()
        {
            var httpResponse = await _client.GetAsync("/api/Usuarios/");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<List<Usuario>>(stringResponse);
            Type type = new TypeDelegator(typeof(List<Usuario>));
            Assert.False(User.GetType() == type);
        }

        [Fact]
        public async Task CanGetUsuarioById()
        {
            var httpResponse = await _client.GetAsync("/api/Usuarios/1");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<Usuario>(stringResponse);
            Type type = new TypeDelegator(typeof(Usuario));
            Assert.False(User.GetType() == type);
        }

        [Fact]
        public async Task CanPostUsuario()
        {
            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nombre","dominic"),
                    new KeyValuePair<string, string>("apellido","di´cococ"),
                    new KeyValuePair<string, string>("email","link_the_hero@hotmail.com"),
                    new KeyValuePair<string, string>("password","8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"),

                });
            var httpResponse = await _client.PostAsync("/api/Usuarios/", formContent);
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<Usuario>(stringResponse);
            Type type = new TypeDelegator(typeof(Usuario));
            Assert.False(User.GetType() == type);
        }
        [Fact]
        public async Task CanPostUser()
        {
            var user = new User() {
                nombre = "Antonio",
                apellido = "Margarete",
                email = "link_the_hero@hotmail.com",
                password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"
            };
            var result = JsonConvert.SerializeObject(user);
            var content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync("/api/Users/", content);
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<User>(stringResponse);
            Type type = new TypeDelegator(typeof(User));
            Assert.False(User.GetType() == type);
        }

    }

}

