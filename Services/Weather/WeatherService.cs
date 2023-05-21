using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using WeatherApp.Data;
using WeatherApp.Data.DataTransferObjects;
using WeatherApp.Data.DataTransferObjects.OpenMeteoDTOs;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories;
using WeatherApp.EntityFramework;

namespace WeatherApp.Services.Weather
{
    public class WeatherService : IWeatherService, IWeatherCacheService
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<WeatherForecastDTO> GetWeatherForecastAsync(WeatherForecastParameters param)
        {
            HttpClient httpClient = new();
            string apiUrl = "https://api.open-meteo.com/v1";
            string requestUrl = $"{apiUrl}/forecast?latitude={param.Latitude}&longitude={param.Longitude}&hourly=temperature_2m,relativehumidity_2m,apparent_temperature,precipitation_probability,surface_pressure,windspeed_10m,winddirection_10m&forecast_days={param.ForecastDays}&timezone=auto";
            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                WeatherForecastResponseDTO weatherForecastResponse = JsonConvert.DeserializeObject<WeatherForecastResponseDTO>(responseBody);
                WeatherForecastDTO forecast = new()
                {
                    TemperatureC = weatherForecastResponse.hourly.temperature_2m,
                    ApparentTemperatureC = weatherForecastResponse.hourly.apparent_temperature,
                    PrecipitationProbability = weatherForecastResponse.hourly.precipitation_probability,
                    WindSpeed = weatherForecastResponse.hourly.windspeed_10m,
                    WindDirection = weatherForecastResponse.hourly.winddirection_10m,
                    RelativeHumidity = weatherForecastResponse.hourly.relativehumidity_2m,
                    SurfacePressure = weatherForecastResponse.hourly.surface_pressure
                };

                ThirdPartyRequest thirdPartyRequest = new()
                {
                    Code = ThirdPartyAPICode.OpenMeteo,
                    Timestamp = DateTime.Now,
                    Parameters = JsonConvert.SerializeObject(param)
                };

                await _weatherRepository.Add(thirdPartyRequest);

                httpClient.Dispose();
                return forecast;
            }
            httpClient.Dispose();
            return null;
        }
    }
}
