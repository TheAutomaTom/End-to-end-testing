using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace E2E.Tests.Integration.Testcontainer_SingleFile
{
  /// <summary> This test does not require any outside setup to connect to a containerized database. </summary>
  public class TestcontainerInstanceTests : IAsyncLifetime
  {
    readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();


    [Fact]
    public async Task ReadFromMsSqlDatabase()
    {

      var cs = _msSqlContainer.GetConnectionString();
      await using var connection = new SqlConnection(cs);
      await connection.OpenAsync();

      await using var command = connection.CreateCommand();
      command.CommandText = "SELECT 1;";

      var actual = await command.ExecuteScalarAsync() as int?;
      Assert.Equal(1, actual.GetValueOrDefault());
    }

    public Task InitializeAsync() => _msSqlContainer.StartAsync();

    public Task DisposeAsync() => _msSqlContainer.DisposeAsync().AsTask();

  }
}
