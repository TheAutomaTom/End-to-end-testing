using System.Text.Json;
using E2E.Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using static TestcontainersModules.StartupTests;

namespace E2E.Tests.Integration
{
  public class ForecastControllerTests : IDisposable
  {
    CustomWebApplicationFactory _factory;
    HttpClient _client;

    public ForecastControllerTests()
    {
      // Calling this first will set the env to "Test"
      var webHost = WebHost.CreateDefaultBuilder().UseStartup<TestStartupBootstrap>().Build();
      
      _factory = new CustomWebApplicationFactory();
      // This called Startup again, but not through `TestStartupBootstrap`... but it still saves the env setting of "Test".
      _client = _factory.CreateClient();

    }

    public void Dispose()
    {
      _factory.Dispose();
      _client.Dispose();

    }


    [Fact]
    public async Task Get_ReturnsForecasts()
    {
      var response = await _client.GetAsync("/WeatherForecast");
      response.EnsureSuccessStatusCode();

      var content = await response.Content.ReadAsStringAsync();
      var forecasts =  JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(content);

      Assert.NotNull(forecasts);
      Assert.True(forecasts.Count() > 0);

    }



  }
}
