using E2E.Api.Models;

namespace E2E.Api.Data
{
  public class ForecastRepository : IForecastRepository
  {
    readonly ForecastDbContext _context;
    readonly ILogger<ForecastRepository> _logger;


    public ForecastRepository(ForecastDbContext context, ILogger<ForecastRepository> logger)
    {
      _context = context;
      _logger = logger;

    }


    public async Task<bool> SaveForecast(WeatherForecast input, CancellationToken ct)
    {
      _context.WeatherForecasts.Add(input);
      return await _context.SaveChangesAsync(ct) == 1;

    }



  }
}
