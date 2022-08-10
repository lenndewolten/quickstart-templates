using Microsoft.AspNetCore.Mvc;

namespace Kubernetes.Api.Template.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
#if (enableApiVersioning)
    [ApiVersion("1.0")]
#endif
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult GetWeatherForecast()
        {
            _logger.LogInformation("Hello world");
            return Ok("Up and running!");
        }
    }
}