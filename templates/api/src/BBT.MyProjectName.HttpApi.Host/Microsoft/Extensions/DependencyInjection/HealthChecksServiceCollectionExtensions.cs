using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection;

public static class HealthChecksServiceCollectionExtensions
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        // Add your health checks here
        var healthChecksBuilder = services.AddHealthChecks();

        // Other HealthChecks can be added here.
        healthChecksBuilder
            .AddNpgSql(configuration.GetConnectionString("Default")!, name: "database", tags: ["ready"]) // Optional
            .AddCheck("self", () => HealthCheckResult.Healthy(), tags: ["live"]);
        
        return services;
    }
}