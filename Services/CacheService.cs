using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;

namespace WeatherApp.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private static readonly ConcurrentDictionary<string, string> _keys = new();

        const int _CACHE_TIMEOUT_H = 6;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken token = default) where T : class
        {
            var result = await _cache.GetAsync<T>(key, token);
            return result;
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken token = default) where T : class
        {
            _keys.TryAdd(key, string.Empty);
            await _cache.SetAsync(key, value, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(_CACHE_TIMEOUT_H) }, token);
        }

        public void Reset()
        {
            lock (_keys)
            {
                foreach (string _key in _keys.Keys)
                {
                    _cache.Remove(_key);
                }
                _keys.Clear();
            }
        }
    }
}
