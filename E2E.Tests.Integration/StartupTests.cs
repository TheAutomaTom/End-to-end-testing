using E2E.Api;
using E2E.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace TestcontainersModules;

public sealed class StartupTests
{
  /// <summary> This test demonstrates that you can extend the Startup class for testing purposes.. </summary>


  [Fact]
  public void StartupTest()
  {    
    var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<MyStartup>().Build();
    Assert.NotNull(webHost);    
    Assert.NotNull(webHost.Services.GetRequiredService<IForecastGenerator>());
    Assert.Equal("Test", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
    Assert.NotNull(webHost.Services.GetRequiredService<IForecastGenerator>());
    Assert.Equal(typeof(ForecastGenerator), webHost.Services.GetRequiredService<IForecastGenerator>().GetType());

  }


  public class MyStartup : Startup
  {
    public MyStartup(IConfiguration config) : base(config) {

      Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

    }
  }

}
