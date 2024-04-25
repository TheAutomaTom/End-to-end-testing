using E2E.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace TestcontainersModules;

public sealed partial class StartupTests
{
  /// <summary> This test demonstrates that you can extend the Startup class for testing purposes.. </summary>


  [Fact]
  public void StartupTest()
  {    
    var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<TestStartupBootstrap>().Build();

    Assert.NotNull(webHost);
    Assert.NotNull(webHost.Services.GetRequiredService<IForecastGenerator>());
    Assert.Equal("Test", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
    Assert.NotNull(webHost.Services.GetRequiredService<IForecastGenerator>());
    Assert.Equal(typeof(ForecastGenerator), webHost.Services.GetRequiredService<IForecastGenerator>().GetType());

  }

}
