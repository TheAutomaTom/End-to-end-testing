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
    readonly IForecastRepository _repo;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastRepository repo)
    {
      _logger = logger;
      _repo = repo;
    }



    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
      var result = await _repo.Get();
      if (result.Any())
      {
        return Ok(result);
      }
        return NoContent();

    }


    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Post([FromBody] WeatherForecast forecast)
    {
      var result = await _repo.SaveForecast(forecast);
      return Ok(result);



    }



  }
}
