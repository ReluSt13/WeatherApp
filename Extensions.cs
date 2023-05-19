using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace WeatherApp
{
    public static class Extensions
    {
        public static async Task SetAsync<T>(this IDistributedCache distributedCache, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default) where T : class
        {
            string val = JsonConvert.SerializeObject(value, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            await distributedCache.SetStringAsync(key, val, options, token);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken token = default) where T : class
        {
            string result = await distributedCache.GetStringAsync(key, token);
            return result == null ? null : JsonConvert.DeserializeObject<T>(result);
        }
    }
}
