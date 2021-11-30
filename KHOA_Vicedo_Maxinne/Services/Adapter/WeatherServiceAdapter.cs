using KHOA_Vicedo_Maxinne.Models;
using KHOA_Vicedo_Maxinne.Utilities;
using Newtonsoft.Json;
using System;
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

            try
            {
                HttpResponseMessage a = await _weatherHttpClient.GetAsync(string.Format("{0}?{1}&query={2}", _getCurrentWeatherUrl,
                        _getAccessKeyApi, query));

                using (HttpContent content = a.Content)
                {
                    string sContent = await content.ReadAsStringAsync();
                    weather = JsonConvert.DeserializeObject<WeatherResponse>(sContent);
                }

                if (weather.success ?? true)
                {
                    Logging.LogInfo($"WeatherService -- GetWeather - Success API Call for zipcode: {query}");
                }
                else
                {
                    Logging.LogError($"WeatherService -- GetWeather - Error API Call for zip code {query} with Error Code: {weather.error.code} {weather.error.info}");
                }
            }

            catch (Exception ex)
            {
                Logging.LogError($"WeatherService -- GetWeather - ZipCode: {query} Error: {ex.Message}");
            }

            return weather;
        }
    }
}
