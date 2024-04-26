using E2E.Api;
using Microsoft.Extensions.Configuration;

namespace E2E.Tests.Integration;

public sealed partial class StartupTests
{
  public class TestStartupBootstrap : Startup
  {
    public TestStartupBootstrap(IConfiguration config) : base(config) {

      // This will be overriden by ForecastControllerTests' constructor setting the env to "ForecastControllerTest"
      Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "TestStartupBootstrap");




    }
  }

}
