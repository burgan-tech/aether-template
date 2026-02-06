using System.ComponentModel.DataAnnotations;
using System.Data;
using BBT.Aether.Application;
using BBT.Aether.Application.Dtos;
using BBT.Aether.Application.Services;
using BBT.Aether.Aspects;
using BBT.Aether.DistributedLock;
using BBT.Aether.Domain.Repositories;
using BBT.Aether.Events;
using BBT.Aether.Results;
using BBT.Aether.Uow;
using BBT.MyProjectName.Issues.Events;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BBT.MyProjectName.Issues;

public sealed class IssueAppService(
    IServiceProvider serviceProvider,
    IIssueRepository issueRepository,
    IDistributedEventBus eventBus,
    IDistributedLockService lockService,
    IUnitOfWorkManager unitOfWorkManager)
    : ApplicationService(serviceProvider), IIssueAppService
{
    public async Task<IssueDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var issue = await issueRepository.GetAsync(id, true, cancellationToken);
        return ObjectMapper.Map<Issue, IssueDto>(issue);
    }

    public async Task<PagedResultDto<IssueDto>> GetListAsync(GetIssuesInput input,
        CancellationToken cancellationToken = default)
    {
        var itemPagedList = await issueRepository.GetPagedListAsync(
            new PaginationParameters()
            {
                Sorting = input.Sorting, SkipCount = input.SkipCount, MaxResultCount = input.MaxResultCount
            }, true, cancellationToken);

        return new PagedResultDto<IssueDto>(itemPagedList.TotalPages,
            ObjectMapper.Map<List<Issue>, List<IssueDto>>(itemPagedList.Items.ToList())
        );
    }

    // [UnitOfWork]
    [Log(LogArguments = true)]
    [Trace]
    public async Task<Result<IssueDto>> CreateAsync([Enrich] Guid repositoryId, CreateIssueInput input,
        CancellationToken cancellationToken = default)
    {
        await using var acquire =
            await lockService.TryAcquireLockAsync(repositoryId.ToString(), cancellationToken: cancellationToken);

        if (acquire == null)
        {
            throw new LockRecursionException();
        }
        
        // return Result<IssueDto>.Fail(Error.Validation("App:1001", "validation hatası", new List<ValidationResult>()
        // {
        //     new ValidationResult("sysys", new []{ "Titlesss" })
        // }));
        
        var issue = new Issue(
            GuidGenerator.Create(),
            repositoryId,
            input.Title,
            input.Text
        ) { Tags = input.Tags };

        Logger.LogInformation("Issue has been created. {IssueId}", issue.Id);

        await issueRepository.InsertAsync(issue, false, cancellationToken);
        
        
        var refreshIssue = await issueRepository.FindAsync(issue.Id,  true, cancellationToken);
        
        
        await lockService.ReleaseLockAsync(repositoryId.ToString(), cancellationToken);
        
        
        return Result<IssueDto>.Ok(ObjectMapper.Map<Issue, IssueDto>(issue));
    }

    public async Task<IssueDto> UpdateAsync(Guid id, UpdateIssueInput input,
        CancellationToken cancellationToken = default)
    {
        var issue = await issueRepository.GetAsync(id, true, cancellationToken);
        issue.SetTitle(input.Title);
        issue.Text = input.Text;
        issue.Tags = input.Tags;
        await issueRepository.UpdateAsync(issue, true, cancellationToken);
        return ObjectMapper.Map<Issue, IssueDto>(issue);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await issueRepository.DeleteAsync(id, true, cancellationToken);
    }

    public async Task CloseAsync(Guid id, CloseIssueInput input, CancellationToken cancellationToken = default)
    {
        var issue = await issueRepository.GetAsync(id, true, cancellationToken);
        issue.Close(input.CloseReason);
        await issueRepository.UpdateAsync(issue, true, cancellationToken);

        // Publish IssueClosedEvent
        var closedEvent = new IssueClosedEvent(
            issue.Id,
            issue.RepositoryId,
            issue.Title,
            input.CloseReason);

        await eventBus.PublishAsync(closedEvent, issue.Id.ToString(), false, cancellationToken);
        Logger.LogInformation("IssueClosedEvent published for Issue {IssueId} with reason {CloseReason}", issue.Id,
            input.CloseReason);
    }

    public async Task ReOpenAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var issue = await issueRepository.GetAsync(id, true, cancellationToken);
        issue.ReOpen();
        await issueRepository.UpdateAsync(issue, true, cancellationToken);
    }

    public async Task AddCommentAsync(Guid id, AddIssueCommentInput input,
        CancellationToken cancellationToken = default)
    {
        var issue = await issueRepository.GetAsync(id, true, cancellationToken);
        issue.AddComment(input.Text, Guid.NewGuid());
        await issueRepository.UpdateAsync(issue, true, cancellationToken);
    }
}