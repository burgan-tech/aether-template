using BBT.Aether.Domain.EntityFrameworkCore;
using BBT.Aether.Domain.Services;
using BBT.Aether.Uow;
using BBT.MyProjectName.Data;
using Microsoft.EntityFrameworkCore;

namespace BBT.MyProjectName.Issues;

public sealed class EfCoreIssueRepository(
    MyProjectNameDbContext dbContext,
    IServiceProvider serviceProvider,
    IAmbientUnitOfWorkAccessor accessor)
    : EfCoreRepository<MyProjectNameDbContext, Issue, Guid>(dbContext, serviceProvider),
        IIssueRepository
{
    public async override Task<IQueryable<Issue>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(p => p.Comments)
            .Include(p => p.Labels);
    }
}