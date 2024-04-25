using E2E.Api.Data;
using E2E.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace E2E.Api.Controllers
{
    [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    readonly ILogger<WeatherForecastController> _logger;
    readonly IForecastGenerator _forecaster;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastGenerator forecaster)
    {
      _logger = logger;
      _forecaster = forecaster;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
      return _forecaster.Generate();
    }
  }
}
