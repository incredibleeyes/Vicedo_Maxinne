using KHOA_Vicedo_Maxinne.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace KHOA_Vicedo_Maxinne.Services.Adapter
{
    public class WeatherServiceAdapter : IWeatherServiceAdapter
    {
        private readonly IWeatherHttpClient _weatherHttpClient;
        private readonly string _getCurrentWeatherUrl;
        private readonly string _getAccessKeyApi;

        public WeatherServiceAdapter(IWeatherHttpClient weatherHttpClient)
        {
            _weatherHttpClient = weatherHttpClient;
            _getCurrentWeatherUrl = ConfigurationManager.AppSettings["CurrentWeatherEndpoint"];
            _getAccessKeyApi = ConfigurationManager.AppSettings["access_key"];

        }

        public async Task<WeatherResponse> GetWeather(string query)
        {
            WeatherResponse weather = new WeatherResponse();
            HttpResponseMessage a = await _weatherHttpClient.GetAsync(string.Format("{0}?{1}&query={2}",_getCurrentWeatherUrl,
                    _getAccessKeyApi,query));

            using (HttpContent content = a.Content)
            {
                string sContent = await content.ReadAsStringAsync();
                weather = JsonConvert.DeserializeObject<WeatherResponse>(sContent); 
            }

            return weather;

        }
    }
}
