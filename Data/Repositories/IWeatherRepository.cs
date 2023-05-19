using WeatherApp.Data.Entities;

namespace WeatherApp.Data.Repositories
{
    public interface IWeatherRepository
    {
        Task<T> Add<T>(T entity);
    }
}
