using System.Net.Http;
using System.Threading.Tasks;

namespace KHOA_Vicedo_Maxinne.Services
{
    public interface IWeatherHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}