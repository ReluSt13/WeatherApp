using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services.Location
{
    public interface IReverseLocationService
    {
        Task<List<ReverseLocationDTO>> GetReverseLocationAsync(ReverseLocationParameters param);
    }
}
