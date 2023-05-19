using Microsoft.EntityFrameworkCore;
using WeatherApp.Data.Entities;
using WeatherApp.EntityFramework;

namespace WeatherApp.Data.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherContext _context;

        public WeatherRepository(WeatherContext context) 
        {
            _context = context;
        }

        public async Task<T> Add<T>(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
