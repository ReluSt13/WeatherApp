using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services.Location
{
    public class ReverseLocationCacheService : IReverseLocationCacheService
    {
        private readonly IReverseLocationCacheService _inner;
        private readonly ICacheService _cache;

        public ReverseLocationCacheService(IReverseLocationCacheService inner, ICacheService cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<List<ReverseLocationDTO>> GetReverseLocationAsync(ReverseLocationParameters param)
        {
            var _key = Utils.GetCachePrefix(CacheKey.ReverseLocation, param.Latitude, param.Longitude);
            List<ReverseLocationDTO> result = await _cache.GetAsync<List<ReverseLocationDTO>>(_key);

            if (result == null)
            {
                result = await _inner.GetReverseLocationAsync(param);
                await _cache.SetAsync(_key, result);
            }

            return result;
        }
    }
}
