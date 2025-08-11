namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for setting up domain services in an <see cref="IServiceCollection" />.
/// </summary>
public static class MyProjectNameDomainModuleServiceCollectionExtensions
{
    /// <summary>
    /// Adds the domain module services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddDomainModule(
        this IServiceCollection services)
    {
        services.AddAetherDomain();
        return services;
    }
}