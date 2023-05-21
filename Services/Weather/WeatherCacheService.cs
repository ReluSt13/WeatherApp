using System.Diagnostics;
using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp.Services.Weather
{
    public class WeatherCacheService : IWeatherCacheService
    {
        private readonly IWeatherCacheService _inner;
        private readonly ICacheService _cache;

        public WeatherCacheService(IWeatherCacheService inner, ICacheService cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<WeatherForecastDTO> GetWeatherForecastAsync(WeatherForecastParameters param)
        {
            var _key = Utils.GetCachePrefix(CacheKey.WeatherForecast, param.Longitude, param.Latitude, param.ForecastDays);
            WeatherForecastDTO result = await _cache.GetAsync<WeatherForecastDTO>(_key);

            if (result == null)
            {
                result = await _inner.GetWeatherForecastAsync(param);
                await _cache.SetAsync(_key, result);
            }

            return result;
        }
    }
}
