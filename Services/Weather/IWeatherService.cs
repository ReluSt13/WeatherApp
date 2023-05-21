using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherForecastDTO> GetWeatherForecastAsync(WeatherForecastParameters param);
    }
}
