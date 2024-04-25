using E2E.Api;
using Microsoft.Extensions.Configuration;

namespace TestcontainersModules;

public sealed partial class StartupTests
{
  public class TestStartupBootstrap : Startup
  {
    public TestStartupBootstrap(IConfiguration config) : base(config) {

      Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");


    }
  }

}
