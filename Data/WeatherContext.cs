using Microsoft.EntityFrameworkCore;
using DashboardApplication.Models;

namespace DashboardApplication.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherData> WeatherData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherData>()
                .HasIndex(w => w.Date);
        }
    }
} 