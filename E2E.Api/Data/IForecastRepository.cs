using E2E.Api.Models;

namespace E2E.Api.Data
{
  public interface IForecastRepository
  {
    Task<IEnumerable<WeatherForecast>> Get();
    Task<bool> SaveForecast(WeatherForecast input);
  }
}