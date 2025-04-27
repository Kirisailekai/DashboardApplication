using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DashboardApplication.Models;
using DashboardApplication.Services;
using Microsoft.EntityFrameworkCore;

namespace DashboardApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly WeatherContext _context;

        public WeatherController(WeatherService weatherService, WeatherContext context)
        {
            _weatherService = weatherService;
            _context = context;
        }

        [HttpGet("{city}")]
        public async Task<ActionResult<WeatherData>> GetCurrentWeather(string city)
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherDataAsync(city);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при получении данных о погоде: {ex.Message}");
            }
        }

        [HttpGet("history/{city}")]
        public async Task<ActionResult<IEnumerable<WeatherData>>> GetWeatherHistory(string city)
        {
            var oneWeekAgo = DateTime.Now.AddDays(-7);
            var history = await _context.WeatherData
                .Where(w => w.City == city && w.Date >= oneWeekAgo)
                .OrderBy(w => w.Date)
                .ToListAsync();

            return Ok(history);
        }
    }
} 