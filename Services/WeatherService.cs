using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using DashboardApplication.Models;
using DashboardApplication.Data;
using Microsoft.Extensions.Configuration;
using System.Net;

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

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("OpenWeather API key is not configured");
            }
        }

        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City name cannot be empty", nameof(city));
            }

            try
            {
                var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
                
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new InvalidOperationException("Invalid OpenWeather API key");
                }
                
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ArgumentException($"City '{city}' not found");
                }

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var weatherResponse = JsonSerializer.Deserialize<OpenWeatherResponse>(content);

                if (weatherResponse == null || weatherResponse.Weather == null || weatherResponse.Weather.Length == 0)
                {
                    throw new InvalidOperationException("Invalid response from OpenWeather API");
                }

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
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Error connecting to OpenWeather API: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Error parsing OpenWeather API response: {ex.Message}", ex);
            }
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