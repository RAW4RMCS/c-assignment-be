using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountApi.ThirdPartyApis
{
    public class RandomFact
    {
            public string Guid { get; set; }
            public string Text { get; set; }
            public string Source { get; set; }
            public string Source_url { get; set; }
            public string Language { get; set; }
            public string Permalink { get; set; }

        
        public static async Task<RandomFact> GetFact()
        {
            var uri = "https://uselessfacts.jsph.pl/random.json?language=en";
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
