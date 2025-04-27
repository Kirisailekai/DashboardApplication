using System;

namespace DashboardApplication.Models
{
    public class WeatherData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double Precipitation { get; set; }
        public string WeatherDescription { get; set; }
        public string City { get; set; }
    }
}
