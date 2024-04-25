using E2E.Api.Models;

namespace E2E.Api.Deprecated
{
    public interface IForecastGenerator
    {
        IEnumerable<WeatherForecast> Generate();
    }
}
