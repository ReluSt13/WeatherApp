using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecastDTO> GetWeatherForecastAsync(WeatherForecastParameters param);
    }
}
