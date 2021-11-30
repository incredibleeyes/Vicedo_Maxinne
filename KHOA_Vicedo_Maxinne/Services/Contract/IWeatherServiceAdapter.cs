using KHOA_Vicedo_Maxinne.Models;
using System.Threading.Tasks;

namespace KHOA_Vicedo_Maxinne.Services.Adapter
{
    public interface IWeatherServiceAdapter
    {
        Task<WeatherResponse> GetWeather(string query);
    }
}