using Microsoft.AspNetCore.Mvc;
using Weather.Models;

namespace Weather.WebApi.Controllers
{
    [ApiController]
    [Route("v1/weather/forecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{city}/{date}")]
        public WeatherForecast Get(string city, DateTime date)
        {
            return new WeatherForecast
            {
                Date = date,
                City = city,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpPost] 
        public void Post(string city, DateTime date)
        {
            
        }
    }
}