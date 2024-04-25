using System.ComponentModel.DataAnnotations;

namespace E2E.Api.Models
{
  public class WeatherForecast
  {
    [Key]
    public int Id { get; set; }
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public ForecastSummary? Summary { get; set; }
  }
}
