using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BBT.MyProjectName.Data;


public sealed class MessagingDbContextDesignFactory: IDesignTimeDbContextFactory<MessagingDbContext>
{
    public MessagingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MessagingDbContext>();
        optionsBuilder.UseNpgsql(GetConnectionStringFromConfiguration(), npgsqlOptions =>
        {
            npgsqlOptions.MigrationsHistoryTable("__MyProjectName_Migrations", "sys_queues");
        });

        return new MessagingDbContext(
            optionsBuilder.Options
        );
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