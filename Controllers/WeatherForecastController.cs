using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeatherApp.Data.DataTransferObjects;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories;
using WeatherApp.EntityFramework;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherCacheService _weatherService;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherForecastController(IWeatherCacheService weatherService, IWeatherRepository weatherRepository)
        {
            _weatherService = weatherService;
            _weatherRepository = weatherRepository;
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
    }
}