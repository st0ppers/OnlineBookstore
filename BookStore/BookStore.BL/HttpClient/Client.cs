using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BookStore.BL.HttpClient
{
    public class Client
    {
        private readonly System.Net.Http.HttpClient _client;
        private readonly IOptionsMonitor<HttpClientSettings> _settings;
        public Client(IOptionsMonitor<HttpClientSettings> settings)
        {
            _settings = settings;
            _client = new System.Net.Http.HttpClient()
            {   
                BaseAddress = new Uri(_settings.CurrentValue.BaseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Dictionary<int, string>?> GetAdditionalInfo()
        {
            var res = await _client.GetStringAsync(_settings.CurrentValue.BaseAddress);
            var authorAddInfo = JsonConvert.DeserializeObject<Dictionary<int, string>>(res);
            return authorAddInfo;
        }
    }
}
