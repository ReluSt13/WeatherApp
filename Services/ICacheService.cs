namespace WeatherApp.Services
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key, CancellationToken token = default) where T : class;
        Task SetAsync<T>(string key, T value, CancellationToken token = default) where T : class;
        void Reset();
    }
}
