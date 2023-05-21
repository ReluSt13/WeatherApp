using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services.Weather
{
    public interface IWeatherCacheService
    {
        Task<WeatherForecastDTO> GetWeatherForecastAsync(WeatherForecastParameters param);
    }
}
