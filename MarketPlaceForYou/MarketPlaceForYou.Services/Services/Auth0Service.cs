using MarketPlaceForYou.Services.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class Auth0Service : IAuth0Service
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Auth0Service(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task BlockUser(string userId)
        {
            //var json = new JsonObject(
            //{
            //    new JsonObject<string,bool>("blocked",true)
            //});

            bool blocked = true;
            //var json = JsonSerializer.Serialize(blocked);
            var json = JsonConvert.SerializeObject(blocked);

            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var url = $"http://marketforyou.com/api/v2/users/{userId}";
            //var request = new HttpRequestMessage(HttpMethod.Patch, $"http://marketforyou.com/api/v2/users/{userId}");
            var httpClient = _httpClientFactory.CreateClient(url);

             HttpResponseMessage response = await httpClient.PatchAsync(url, content);


        }

        public async Task UnblockUser(string userId)
        {

            bool blocked = false;
            var json = JsonConvert.SerializeObject(blocked);

            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var url = $"http://marketforyou.com/api/v2/users/{userId}";
            //var request = new HttpRequestMessage(HttpMethod.Patch, $"http://marketforyou.com/api/v2/users/{userId}");
            var httpClient = _httpClientFactory.CreateClient(url);

            using (var client = httpClient)
            {
                HttpResponseMessage response = await httpClient.PatchAsync(url, content);
            }

        }
    }
}
