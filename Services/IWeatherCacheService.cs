using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services
{
    public interface IWeatherCacheService
    {
        Task<WeatherForecastDTO> GetWeatherForecastAsync(WeatherForecastParameters param);
    }
}
