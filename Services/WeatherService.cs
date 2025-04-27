using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using DashboardApplication.Models;
using DashboardApplication.Data;
using Microsoft.Extensions.Configuration;

namespace DashboardApplication.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly WeatherContext _context;

        public WeatherService(HttpClient httpClient, IConfiguration configuration, WeatherContext context)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenWeather:ApiKey"];
            _context = context;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var weatherResponse = JsonSerializer.Deserialize<OpenWeatherResponse>(content);

            var weatherData = new WeatherData
            {
                Date = DateTime.Now,
                Temperature = weatherResponse.Main.Temp,
                Humidity = weatherResponse.Main.Humidity,
                WindSpeed = weatherResponse.Wind.Speed,
                WeatherDescription = weatherResponse.Weather[0].Description,
                City = city
            };

            await _context.WeatherData.AddAsync(weatherData);
            await _context.SaveChangesAsync();

            return weatherData;
        }

        private class OpenWeatherResponse
        {
            public Main Main { get; set; }
            public Wind Wind { get; set; }
            public Weather[] Weather { get; set; }
        }

        private class Main
        {
            public double Temp { get; set; }
            public double Humidity { get; set; }
        }

        private class Wind
        {
            public double Speed { get; set; }
        }

        private class Weather
        {
            public string Description { get; set; }
        }
    }
} 