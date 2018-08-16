using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.TestWiremock
{
    public class MySuperService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public MySuperService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            
        }

        public async Task<string> MakeAsyncCall() 
        {
            using(var response = await _httpClient.GetAsync(_baseUrl))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
