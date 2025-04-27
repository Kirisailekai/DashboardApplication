using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DashboardApplication.Models;
using DashboardApplication.Services;
using DashboardApplication.Data;
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
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("{city}")]
        public async Task<ActionResult<WeatherData>> GetCurrentWeather(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("City name cannot be empty");
            }

            try
            {
                var weatherData = await _weatherService.GetWeatherDataAsync(city);
                return Ok(weatherData);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("history/{city}")]
        public async Task<ActionResult<IEnumerable<WeatherData>>> GetWeatherHistory(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("City name cannot be empty");
            }

            try
            {
                var oneWeekAgo = DateTime.Now.AddDays(-7);
                var history = await _context.WeatherData
                    .Where(w => w.City == city && w.Date >= oneWeekAgo)
                    .OrderBy(w => w.Date)
                    .ToListAsync();

                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving weather history: {ex.Message}");
            }
        }
    }
} 