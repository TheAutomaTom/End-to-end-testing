using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using E2E.Api.Deprecated;

namespace E2E.Tests.Integration;

public sealed partial class StartupTests
{
  /// <summary> This test demonstrates that you can extend the Startup class for testing purposes.. </summary>


  [Fact]
  public void StartupTest()
  {    
    var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<TestStartupBootstrap>().Build();

    Assert.NotNull(webHost);

    // This is set in TestStartupBootstrap's constructor.
    Assert.Equal("TestStartupBootstrap", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

    Assert.NotNull(webHost.Services.GetRequiredService<IForecastGenerator>());
    Assert.NotNull(webHost.Services.GetRequiredService<IForecastGenerator>());
    Assert.Equal(typeof(ForecastGenerator), webHost.Services.GetRequiredService<IForecastGenerator>().GetType());

  }

}
