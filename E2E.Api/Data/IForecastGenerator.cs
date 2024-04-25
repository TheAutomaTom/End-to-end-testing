using E2E.Api.Models;

namespace E2E.Api.Data
{
  public interface IForecastGenerator
  {
    IEnumerable<WeatherForecast> Generate();
  }
}