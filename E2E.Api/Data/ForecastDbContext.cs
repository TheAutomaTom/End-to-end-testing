using E2E.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace E2E.Api.Data
{
  public class ForecastDbContext : DbContext
  {
    readonly IConfiguration _config;
    readonly string _connectionString;

    public ForecastDbContext(IConfiguration config, DbContextOptions<ForecastDbContext> options) : base(options)
    {
      _config = config;
      var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      if ( env == "Test")
      {
        _connectionString = Environment.GetEnvironmentVariable("TestcontainerCS");
      } 
      else 
      {
        _connectionString = $"{_config.GetConnectionString("TypicalConnectionString")}";
      }


      try
      {
        var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        if (databaseCreator != null)
        {
          if (!databaseCreator.CanConnect())
          {
            databaseCreator.Create();
          }
          if (!databaseCreator.HasTables())
          {
            databaseCreator.CreateTables();
          }

        }
      }
      catch (Exception ex)
      {
        throw;
      }

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(_connectionString);
      optionsBuilder.EnableSensitiveDataLogging();
    }


    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder model)
    {
      base.OnModelCreating(model);
    }



  }
}
