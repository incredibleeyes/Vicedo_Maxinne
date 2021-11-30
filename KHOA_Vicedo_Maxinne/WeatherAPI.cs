using KHOA_Vicedo_Maxinne.Constants;
using KHOA_Vicedo_Maxinne.Models;
using KHOA_Vicedo_Maxinne.Services;
using KHOA_Vicedo_Maxinne.Services.Adapter;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KHOA_Vicedo_Maxinne
{
    public class WeatherAPI
    {
        private readonly IWeatherServiceAdapter _weatherServiceAdapter;
        public WeatherAPI(IWeatherServiceAdapter weatherServiceAdapter)
        {
            _weatherServiceAdapter = weatherServiceAdapter;
        }
        public static void Main(string[] args)
        {
            WeatherServiceAdapter serviceAdapter = new WeatherServiceAdapter(new WeatherHttpClient());
            WeatherAPI weatherAPI = new WeatherAPI(serviceAdapter);
            WeatherResponse weatherToday = new WeatherResponse();

            Console.WriteLine("Good day! Today is: " + DateTime.Now.ToLongDateString());


            string zipcode = string.Empty;

            while (String.IsNullOrEmpty(zipcode))
            {
                Console.WriteLine("May I have your zipcode to check on weather conditions?");
                zipcode = Console.ReadLine();

                if (Regex.IsMatch(zipcode, ValidationConstants.RegexZipCode))
                {
                    weatherToday = weatherAPI.GetWeather(zipcode).GetAwaiter().GetResult();

                    Console.WriteLine("The current weather condition today is:" + weatherToday.current.weather_descriptions[0]);

                    if (weatherToday.current.precip > WeatherConstants.slightRainPrecipitaion)
                    {
                        Console.WriteLine("The precipitation level today is at " + weatherToday.current.precip.ToString() + " so we advise you not to go out.");
                    }
                    else
                    {
                        Console.WriteLine("It is a good day to go out today. ");
                    }

                    if (weatherToday.current.uv_index > WeatherConstants.highUvIndex)
                    {
                        Console.WriteLine("UV index is at " + weatherToday.current.uv_index.ToString() + " kindly do not forget your sunscreen");
                    }
                    else
                    {
                        Console.WriteLine("No need for sunscreens today.");
                    }

                    if (weatherToday.current.wind_speed > WeatherConstants.windSpeedForKite)
                    {
                        Console.WriteLine("Bring your kite wind speed is at " + weatherToday.current.wind_speed.ToString());
                    }
                    else
                    {
                        Console.WriteLine("It is not a good day to fly kites today.");
                    }

                }
                else if (zipcode.ToUpper().Equals("N"))
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Please try again and enter a valid zipcode.");
                    Console.WriteLine("Kindly input N if you want to quit.");
                }
            }

            Console.ReadLine();
        }

        
        public async Task<WeatherResponse> GetWeather(string zipcode)
        {
            return await _weatherServiceAdapter.GetWeather(zipcode);
        }


    }
}
