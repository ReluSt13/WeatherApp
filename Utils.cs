using WeatherApp.Data.DataTransferObjects;

namespace WeatherApp
{
    public static class Utils
    {
        public static string GetCachePrefix(string prefix, params object[] parameteres) => 
            $"{prefix}{string.Join("_", parameteres.Select(x => x.ToString()))}";
    }
}
