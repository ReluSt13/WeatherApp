using Newtonsoft.Json;
using WeatherApp.Data;
using WeatherApp.Data.DataTransferObjects;
using WeatherApp.Data.DataTransferObjects.OpenMeteoDTOs;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories;

namespace WeatherApp.Services.Location
{
    public class LocationService : ILocationService, ILocationCacheService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IConfiguration _configuration;

        public LocationService(IWeatherRepository weatherRepository, IConfiguration configuration)
        {
            _weatherRepository = weatherRepository;
            _configuration = configuration; 
        }

        public async Task<List<LocationDTO>> GetLocationAsync(string cityName)
        {
            HttpClient httpClient = new();
            string apiUrl = "https://api.api-ninjas.com/v1";
            string requestUrl = $"{apiUrl}/city?min_population=10000&limit=5&name={cityName}";
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", _configuration["APIKey"]);
            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                List<LocationDTO> location = JsonConvert.DeserializeObject<List<LocationDTO>>(responseBody);

                ThirdPartyRequest thirdPartyRequest = new()
                {
                    Code = ThirdPartyAPICode.APINinjas_Geolocation,
                    Timestamp = DateTime.Now,
                    Parameters = cityName
                };
                await _weatherRepository.Add(thirdPartyRequest);
                httpClient.Dispose();
                return location;
            }
            httpClient.Dispose();
            return null;
        }
    }
}
