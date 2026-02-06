using BBT.Aether.Domain.EntityFrameworkCore;
using BBT.MyProjectName.Data;

namespace BBT.MyProjectName.Issues;

public sealed class EfCoreGitRepository(
    MyProjectNameDbContext dbContext,
    IServiceProvider serviceProvider
)
    : EfCoreRepository<MyProjectNameDbContext, GitRepository, Guid>(
        dbContext, serviceProvider), IGitRepository;