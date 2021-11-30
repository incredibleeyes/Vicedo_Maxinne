using KHOA_Vicedo_Maxinne.Constants;
using KHOA_Vicedo_Maxinne.Models;
using KHOA_Vicedo_Maxinne.Services;
using KHOA_Vicedo_Maxinne.Services.Adapter;
using System;
using System.Text.RegularExpressions;

namespace KHOA_Vicedo_Maxinne
{

    public class WeatherAPI
    {
        public static void Main(string[] args)
        {
            WeatherServiceAdapter serviceAdapter = new WeatherServiceAdapter(new WeatherHttpClient());
            WeatherResponse weatherToday = new WeatherResponse();

            Console.WriteLine("Good day! Today is: " + DateTime.Now.ToLongDateString());


            string zipcode = string.Empty;

            while (String.IsNullOrEmpty(zipcode))
            {
                Console.WriteLine("May I have your zipcode to check on weather conditions?");
                zipcode = Console.ReadLine();

                if (Regex.IsMatch(zipcode, ValidationConstants.RegexZipCode))
                {
                    weatherToday = serviceAdapter.GetWeather(zipcode).GetAwaiter().GetResult();

                    if (weatherToday.success ?? true && weatherToday.request != null)
                    {
                        Console.WriteLine($"The current weather condition today in {weatherToday.location.region} is {weatherToday.current.weather_descriptions[0]}");

                        if (weatherToday.current.precip > WeatherConstants.slightRainPrecipitaion)
                        {
                            Console.WriteLine($"The precipitation level today is at {weatherToday.current.precip} so we advise you not to go out.");
                        }
                        else
                        {
                            Console.WriteLine("It is a good day to go out today. ");
                        }

                        if (weatherToday.current.uv_index > WeatherConstants.highUvIndex)
                        {
                            Console.WriteLine($"UV index is at {weatherToday.current.uv_index} kindly do not forget your sunscreen");
                        }
                        else
                        {
                            Console.WriteLine("No need for sunscreens today.");
                        }

                        if (weatherToday.current.wind_speed > WeatherConstants.windSpeedForKite)
                        {
                            Console.WriteLine($"You can fly your kite today, wind speed is at {weatherToday.current.wind_speed}");
                        }
                        else
                        {
                            Console.WriteLine("It is not a good day to fly kites today.");
                        } 
                    }

                    else
                    {
                        zipcode = String.Empty;
                        Console.WriteLine("There seems to be a problem retrieving that data. Please try another zip code.");
                    }

                }
                else if (zipcode.ToUpper().Equals("X"))
                {
                    Environment.Exit(0);
                }
                else
                {
                    zipcode = String.Empty;
                    Console.WriteLine("Please try again and enter a valid zipcode.");
                    Console.WriteLine("Kindly input X if you want to quit.");
                }
            }

            Console.ReadLine();
        }
    }
}
