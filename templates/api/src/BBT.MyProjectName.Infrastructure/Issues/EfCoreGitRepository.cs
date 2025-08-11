using BBT.Aether.Domain.EntityFrameworkCore;
using BBT.Aether.Domain.Services;
using BBT.MyProjectName.Data;

namespace BBT.MyProjectName.Issues;

public sealed class EfCoreGitRepository(
    MyProjectNameDbContext dbContext, 
    IServiceProvider serviceProvider,
    ITransactionService transactionService
    )
    : EfCoreRepository<MyProjectNameDbContext, GitRepository, Guid>(
        dbContext, serviceProvider, transactionService), IGitRepository;