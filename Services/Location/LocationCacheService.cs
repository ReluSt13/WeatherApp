using WeatherApp.Data.DataTransferObjects;
using WeatherApp.Services.Weather;

namespace WeatherApp.Services.Location
{
    public class LocationCacheService : ILocationCacheService
    {
        private readonly ILocationCacheService _inner;
        private readonly ICacheService _cache;

        public LocationCacheService(ILocationCacheService inner, ICacheService cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<List<LocationDTO>> GetLocationAsync(string cityName)
        {
            var _key = Utils.GetCachePrefix(CacheKey.Location, cityName);
            List<LocationDTO> result = await _cache.GetAsync<List<LocationDTO>>(_key);

            if (result == null)
            {
                result = await _inner.GetLocationAsync(cityName);
                await _cache.SetAsync(_key, result);
            }

            return result;
        }
    }
}
