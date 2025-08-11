using System.IO.Compression;
using System.Text.Json.Serialization;
using BBT.Aether.AspNetCore.ExceptionHandling;
using BBT.Aether.Domain.Services;
using BBT.MyProjectName.Data;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class MyProjectNameApiServiceCollectionExtensions
{
    public static IServiceCollection AddApiHostModule(
        this IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        ConfigureClient(services);
        ConfigureModules(services, configuration);
        ConfigureDbContext(services, configuration);
        ConfigureMapper(services);
        ConfigureTelemetry(services, configuration);
        ConfigureDistributedCache(services);
        ConfigureRedis(services);
        ConfigureHealthChecks(services);
        ConfigureRoute(services);
        return services;
    }

    private static void ConfigureClient(IServiceCollection services)
    {
        services.AddDaprClient();
        services.AddHttpClient();
    }

    private static void ConfigureModules(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAetherCore(options =>
        {
            options.Environment ??= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            options.ApplicationName ??= configuration.GetValue<string?>("ApplicationName") ?? "MyProjectName";
        });
        services.AddInfrastructureModule();
        services.AddAetherAspNetCore();
    }

    private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAetherDbContext<MyProjectNameDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"), npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("__MyProjectName_Migrations");
                // Enable retrying failed database operations
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            });
        });

        services.AddSingleton<IDataSeedService, MyProjectNameDataSeedService>();
    }

    private static void ConfigureMapper(IServiceCollection services)
    {
        services.AddAetherAutoMapperMapper(
        [
            typeof(Program), // ApiHost
            typeof(MyProjectNameDomainModuleServiceCollectionExtensions), // Domain
            typeof(MyProjectNameApplicationModuleServiceCollectionExtensions) // Application
        ]);
    }

    private static void ConfigureTelemetry(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFrameworkTelemetry(configuration);
    }

    private static void ConfigureDistributedCache(IServiceCollection services)
    {
        // Option 1: Use .NET Core Distributed Cache
        services.AddNetCoreDistributedCache(sc =>
        {
            // Configure your preferred distributed cache implementation
            sc.AddDistributedMemoryCache(); // Default in-memory
        });
    }

    private static void ConfigureRedis(IServiceCollection services)
    {
        // Option 2: Use Dapr State Store Cache
        // services.AddDaprStateStoreCache();

        // Add Redis Configuration
        services.AddRedis();
    }

    private static void ConfigureHealthChecks(IServiceCollection services)
    {
        services.AddAppHealthChecks();
    }

    private static void ConfigureRoute(IServiceCollection services)
    {
        // Add services to the container
        services.AddEndpointsApiExplorer();

        // Add API Versioning using the custom configuration
        services.AddAetherApiVersioning(apiTitle: "MyProjectName API");
        
        // Add services to the container.
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                // options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
    }
}