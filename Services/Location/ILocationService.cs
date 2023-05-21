using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services.Location
{
    public interface ILocationService
    {
        Task<List<LocationDTO>> GetLocationAsync(string cityName);
    }
}
