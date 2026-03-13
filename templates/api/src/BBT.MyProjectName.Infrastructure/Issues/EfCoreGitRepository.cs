using BBT.Aether.Domain.EntityFrameworkCore;
using BBT.Aether.Domain.Services;
using BBT.MyProjectName.Data;

namespace BBT.MyProjectName.Issues;

public sealed class EfCoreGitRepository(
    IDbContextProvider<MyProjectNameDbContext> dbContext
)
    : EfCoreRepository<MyProjectNameDbContext, GitRepository, Guid>(
        dbContext), IGitRepository;