using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectPhoenix.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpContextAccessor _accessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _accessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            var userId = _accessor.HttpContext.User.Identity.Name;
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                UserID = userId
            })
            .ToArray();
        }
    }
}
