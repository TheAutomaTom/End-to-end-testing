using E2E.Api.Models;

namespace E2E.Api.Data
{
  public class ForecastGenerator : IForecastGenerator
  {
    Array _summaries = Enum.GetValues(typeof(ForecastSummary));
    Random _random = new Random();

    public IEnumerable<WeatherForecast> Generate()
    {

      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = (ForecastSummary)_summaries.GetValue(_random.Next(_summaries.Length))!
      })
      .ToArray();

    }
  }
}
