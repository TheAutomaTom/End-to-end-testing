using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace E2E.Tests.Integration
{
  /// <summary> This test does not require any outside setup to connect to a containerized database. </summary>
  public class TestcontainerInstanceTests : IAsyncLifetime
  {
    readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    [Fact]
    public async Task ReadFromMsSqlDatabase()
    {
      await using var connection = new SqlConnection(_msSqlContainer.GetConnectionString());
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
