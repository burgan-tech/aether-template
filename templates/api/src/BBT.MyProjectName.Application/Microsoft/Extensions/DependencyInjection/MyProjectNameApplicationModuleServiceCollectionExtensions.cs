using BBT.MyProjectName.GitRepositories;
using BBT.MyProjectName.Issues;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for setting up application services in an <see cref="IServiceCollection" />.
/// </summary>
public static class MyProjectNameApplicationModuleServiceCollectionExtensions
{
    /// <summary>
    /// Adds the application module services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddApplicationModule(
        this IServiceCollection services)
    {
        services.AddDomainModule();
        services.AddAetherApplication();

        // You can register your application service here.
        services.AddScoped<IIssueAppService, IssueAppService>();
        services.AddScoped<IGitRepositoryAppService, GitRepositoryAppService>();
        return services;
    }
}