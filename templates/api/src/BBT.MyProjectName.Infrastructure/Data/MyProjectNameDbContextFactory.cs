using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BBT.MyProjectName.Data;

public class MyProjectNameDbContextFactory : IDesignTimeDbContextFactory<MyProjectNameDbContext>
{
    public MyProjectNameDbContext CreateDbContext(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var optionsBuilder = new DbContextOptionsBuilder<MyProjectNameDbContext>();
        optionsBuilder.UseNpgsql(GetConnectionStringFromConfiguration(), npgsqlOptions =>
        {
            npgsqlOptions.MigrationsHistoryTable("__MyProjectName_Migrations");
            npgsqlOptions.EnableRetryOnFailure(3);
        });

        return new MyProjectNameDbContext(optionsBuilder.Options);
    }

    private static string? GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString("Default");
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"..{Path.DirectorySeparatorChar}BBT.MyProjectName.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json",
                optional: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}