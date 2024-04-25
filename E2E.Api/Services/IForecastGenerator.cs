using E2E.Api.Models;

namespace E2E.Api.Services
{
    public interface IForecastGenerator
    {
        IEnumerable<WeatherForecast> Generate();
    }
}