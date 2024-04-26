using E2E.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace E2E.Tests.Integration.Api_to_Testcontainer_Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {

        public CustomWebApplicationFactory()
        {

        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                // Override typical behavior here...
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

            });
        }


    }
}
