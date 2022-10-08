using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Population.SQLite.App.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Population.SQLite.App.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IApplicationDBContext _applicationDBContext;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IApplicationDBContext applicationDBContext)
        {
            _logger = logger;
            _applicationDBContext = applicationDBContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var a = _applicationDBContext.Dept.ToList();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
