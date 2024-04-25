using E2E.Api.Data;
using E2E.Api.Models;
using E2E.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace E2E.Api.Controllers
{
    [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    readonly ILogger<WeatherForecastController> _logger;
    readonly IForecastGenerator _forecaster;
    readonly IForecastRepository _repo;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastGenerator forecaster, IForecastRepository repo)
    {
      _logger = logger;
      _forecaster = forecaster;
      _repo = repo;
    }



    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
      return _forecaster.Generate();
    }


    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Post([FromBody] WeatherForecast forecast)
    {
      var result = await _repo.SaveForecast(forecast);
      return Ok(result);



    }



  }
}
