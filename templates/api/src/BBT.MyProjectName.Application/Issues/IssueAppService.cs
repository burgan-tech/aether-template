using BBT.Aether.Application.Dtos;
using BBT.Aether.Application.Services;
using BBT.Aether.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace BBT.MyProjectName.Issues;

public sealed class IssueAppService(
    IServiceProvider serviceProvider,
    IIssueRepository issueRepository)
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

    public async Task<IssueDto> CreateAsync(Guid repositoryId, CreateIssueInput input,
        CancellationToken cancellationToken = default)
    {
        var issue = new Issue(
            GuidGenerator.Create(),
            repositoryId,
            input.Title,
            input.Text
        ) { Tags = input.Tags };

        Logger.LogInformation("Issue has been created. {IssueId}", issue.Id);

        await issueRepository.InsertAsync(issue, true, cancellationToken);
        return ObjectMapper.Map<Issue, IssueDto>(issue);
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
        issue.ConcurrencyStamp = input.ConcurrencyStamp;
        await issueRepository.UpdateAsync(issue, true, cancellationToken);
    }
}