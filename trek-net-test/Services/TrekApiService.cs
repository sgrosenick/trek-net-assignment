using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using trek_net_test.Models;

namespace trek_net_test.Services
{
    public class TrekApiService : ITrekApiService
    {
        private readonly HttpClient client;

        public TrekApiService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("TrekApi");
        }

        public async Task<List<Bikes>> GetBikes()
        {
            string url = "interview/bikes.json";
            var result = new List<Bikes>();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<List<Bikes>>(stringResponse);

            } else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
