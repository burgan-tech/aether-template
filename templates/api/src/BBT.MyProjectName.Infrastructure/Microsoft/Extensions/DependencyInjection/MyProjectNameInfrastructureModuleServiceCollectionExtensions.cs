using BBT.MyProjectName.Issues;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for setting up infrastructure services in an <see cref="IServiceCollection" />.
/// </summary>
public static class MyProjectNameInfrastructureModuleServiceCollectionExtensions
{
     /// <summary>
    /// Adds the infrastructure module services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddInfrastructureModule(
        this IServiceCollection services)
    {
        services.AddApplicationModule();
        services.AddAetherInfrastructure();
        
        // You can register your repositories here.
        services.AddScoped<IGitRepository, EfCoreGitRepository>();
        services.AddScoped<IIssueRepository, EfCoreIssueRepository>();

        return services;
    }
}