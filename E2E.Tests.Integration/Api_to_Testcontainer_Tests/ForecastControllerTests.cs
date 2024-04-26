using System.Text;
using System.Text.Json;
using E2E.Api.Deprecated;
using E2E.Api.Models;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace E2E.Tests.Integration.Api_to_Testcontainer_Tests
{
    public class ForecastControllerTests : IDisposable
    {
        CustomWebApplicationFactory _factory;
        HttpClient _client;
        MsSqlContainer _msSqlContainer;
        Random _rando = new Random();

        public ForecastControllerTests()
        {
            // This ctor sets the environment variable.
            _factory = new CustomWebApplicationFactory();

            _msSqlContainer = new MsSqlBuilder().Build();
            _msSqlContainer.StartAsync().Wait();
            var cs = _msSqlContainer.GetConnectionString();
            Environment.SetEnvironmentVariable("TestcontainerCS", cs);

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
