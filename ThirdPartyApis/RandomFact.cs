using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountApi.ThirdPartyApis
{
    public class RandomFact
    {
            public string Data { get; set; }

        public static async Task<RandomFact> GetFact()
        {
            var uri = "https://useless-facts.sameerkumar.website/api";
            // var uri = "https://uselessfacts.jsph.pl/random.json?language=en"; SSL certificate expired
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept", "application/json");

            
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStreamAsync();
            client.Dispose();

            if (!response.IsSuccessStatusCode)
                return null;

            return await JsonSerializer.DeserializeAsync<RandomFact>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
}
