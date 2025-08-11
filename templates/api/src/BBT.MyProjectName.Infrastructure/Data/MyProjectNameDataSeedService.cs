using BBT.Aether.Domain.Services;

namespace BBT.MyProjectName.Data;

/// <summary>
/// This service is responsible for seeding data into the repositories.
/// It implements the <see cref="IDataSeedService"/> interface.
/// </summary>
public class MyProjectNameDataSeedService : IDataSeedService
{
    /// <summary>
    /// Seeds the data asynchronously.
    /// </summary>
    /// <param name="context">The seed context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task SeedAsync(SeedContext context)
    {
        return Task.CompletedTask;
    }
}