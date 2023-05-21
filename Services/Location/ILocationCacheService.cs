using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services.Location
{
    public interface ILocationCacheService
    {
        Task<List<LocationDTO>> GetLocationAsync(string cityName);
    }
}
