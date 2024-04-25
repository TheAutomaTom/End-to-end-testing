using E2E.Api.Models;

namespace E2E.Api.Data
{
  public interface IForecastRepository
  {
    Task<bool> SaveForecast(WeatherForecast input, CancellationToken ct);
  }
}