using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using WeatherApp.Data.Entities;

namespace WeatherApp.EntityFramework
{
    public class WeatherContext : DbContext
    {
        public DbSet<AppRequest> AppRequests { get; set; }
        public DbSet<ThirdPartyRequest> ThirdPartyRequests { get; set; }

        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
