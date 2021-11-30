using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace KHOA_Vicedo_Maxinne.Services
{
    public class WeatherHttpClient : IWeatherHttpClient
    {
        private readonly HttpClient _httpClient;

        public WeatherHttpClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["WeatherAPIBaseAddress"]);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _httpClient.GetAsync(requestUri);
        }
    }
}
