using Newtonsoft.Json;
using WeatherApp.Data.DataTransferObjects;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories;
using WeatherApp.Data;

namespace WeatherApp.Services.Location
{
    public class ReverseLocationService : IReverseLocationService, IReverseLocationCacheService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IConfiguration _configuration;

        public ReverseLocationService(IWeatherRepository weatherRepository, IConfiguration configuration)
        {
            _weatherRepository = weatherRepository;
            _configuration = configuration;
        }

        public async Task<List<ReverseLocationDTO>> GetReverseLocationAsync(ReverseLocationParameters param)
        {
            HttpClient httpClient = new();
            string apiUrl = "https://api.api-ninjas.com/v1";
            string requestUrl = $"{apiUrl}/reversegeocoding?lat={param.Latitude}&lon={param.Longitude}";
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", _configuration["APIKey"]);
            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                List<ReverseLocationDTO> location = JsonConvert.DeserializeObject<List<ReverseLocationDTO>>(responseBody);

                ThirdPartyRequest thirdPartyRequest = new()
                {
                    Code = ThirdPartyAPICode.APINinjas_Reverse_Geolocation,
                    Timestamp = DateTime.Now,
                    Parameters = JsonConvert.SerializeObject(param)
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
