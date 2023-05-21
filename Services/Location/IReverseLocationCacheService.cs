using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services.Location
{
    public interface IReverseLocationCacheService
    {
        Task<List<ReverseLocationDTO>> GetReverseLocationAsync(ReverseLocationParameters param);
    }
}
