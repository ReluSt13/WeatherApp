using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeatherApp.Data.DataTransferObjects;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories;
using WeatherApp.EntityFramework;
using WeatherApp.Services.Weather;
using WeatherApp.Services.Location;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherCacheService _weatherService;
        private readonly ILocationCacheService _locationService;
        private readonly IReverseLocationCacheService _reverseLocationService;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherForecastController(IWeatherCacheService weatherService, IWeatherRepository weatherRepository, ILocationCacheService locationService, IReverseLocationCacheService reverseLocationService)
        {
            _weatherService = weatherService;
            _weatherRepository = weatherRepository;
            _locationService = locationService;
            _reverseLocationService = reverseLocationService;
        }

        [HttpGet]
        public async Task<WeatherForecastDTO> GetTodaysWeather([FromQuery] WeatherForecastParameters parameters)
        {
            AppRequest appRequest = new()
            {
                Ip = Request.Host.Value,
                Action = Request.Path.Value,
                Parameters = JsonConvert.SerializeObject(parameters),
                Timestamp = DateTime.Now
            };
            await _weatherRepository.Add(appRequest);
            return await _weatherService.GetWeatherForecastAsync(parameters);
        }

        [HttpGet("getLocation")]
        public async Task<List<LocationDTO>> GetLocation([FromQuery] string cityName)
        {
            AppRequest appRequest = new()
            {
                Ip = Request.Host.Value,
                Action = Request.Path.Value,
                Parameters = cityName,
                Timestamp = DateTime.Now
            };
            await _weatherRepository.Add(appRequest);
            return await _locationService.GetLocationAsync(cityName);
        }

        [HttpGet("getReverseLocation")]
        public async Task<List<ReverseLocationDTO>> GetReverseLocation([FromQuery] ReverseLocationParameters parameters)
        {
            AppRequest appRequest = new()
            {
                Ip = Request.Host.Value,
                Action = Request.Path.Value,
                Parameters = JsonConvert.SerializeObject(parameters),
                Timestamp = DateTime.Now
            };
            await _weatherRepository.Add(appRequest);
            return await _reverseLocationService.GetReverseLocationAsync(parameters);
        }
    }
}