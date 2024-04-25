using System.Text;
using System.Text.Json;
using E2E.Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using static E2E.Tests.Integration.StartupTests;
using E2E.Api.Deprecated;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http.HttpResults;

namespace E2E.Tests.Integration
{
  public class ForecastControllerTests : IDisposable
  {
    CustomWebApplicationFactory _factory;
    HttpClient _client;
    MsSqlContainer _msSqlContainer;
    Random _rando = new Random();

    public ForecastControllerTests()
    {
      // Calling this first will set the env.
      var webHost = WebHost.CreateDefaultBuilder().UseStartup<TestStartupBootstrap>().Build();

      // This ctor will override the env set above.
      _factory = new CustomWebApplicationFactory();

      _msSqlContainer = new MsSqlBuilder().Build();
      _msSqlContainer.StartAsync().Wait();
      var cs = _msSqlContainer.GetConnectionString();
      Environment.SetEnvironmentVariable("TestcontainerCS", cs);

      // This called Startup again, but not through `TestStartupBootstrap`... but it still saves the env setting of "Test".
      _client = _factory.CreateClient();

    }

    public void Dispose()
    {
      _factory.Dispose();
      _client.Dispose();

    }



    [Fact]
    public async Task Post_PostsForecast()
    {
      var generator = _factory.Services.GetRequiredService<IForecastGenerator>();
      var forecast = generator.Generate().First();

      var jsonContent = new StringContent(JsonSerializer.Serialize(forecast), Encoding.UTF8, "application/json");
      var response = await _client.PostAsync("/WeatherForecast", jsonContent);
      response.EnsureSuccessStatusCode();

      var content = await response.Content.ReadAsStringAsync();

      Assert.Equal("true", content);

    }


    public async Task GetWorksAfterPosts()
    {
      var generator = _factory.Services.GetRequiredService<IForecastGenerator>();

      var toPost = generator.Generate();

      foreach (var forecast in toPost)
      {
        var jsonContent = new StringContent(JsonSerializer.Serialize(forecast), Encoding.UTF8, "application/json");
        var res = await _client.PostAsync("/WeatherForecast", jsonContent);
        res.EnsureSuccessStatusCode();
      }

      var response = await _client.GetAsync("/WeatherForecast");
      response.EnsureSuccessStatusCode();

      var content = await response.Content.ReadAsStringAsync();
      var forecasts = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(content);

      Assert.NotNull(forecasts);
      Assert.Equal(toPost.Count(), forecasts.Count());
    }



  }
}
