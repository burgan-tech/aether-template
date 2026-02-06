using BBT.Aether.Events;

namespace BBT.MyProjectName.Issues.Events;

/// <summary>
/// Event published when a new issue is created.
/// </summary>
[EventName(name: "IssueCreated", topic: "issue.created")]
public class IssueCreatedEvent: IDistributedEvent
{
    /// <summary>
    /// The unique identifier of the created issue.
    /// </summary>
    [EventSubject]
    public Guid IssueId { get; set; }

    /// <summary>
    /// The repository ID where the issue was created.
    /// </summary>
    public Guid RepositoryId { get; set; }

    /// <summary>
    /// The title of the issue.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The text/description of the issue.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// The tags associated with the issue.
    /// </summary>
    public string[]? Tags { get; set; }

    public IssueCreatedEvent()
    {
    }

    public IssueCreatedEvent(
        Guid issueId,
        Guid repositoryId,
        string title,
        string? text,
        string[]? tags)
    {
        IssueId = issueId;
        RepositoryId = repositoryId;
        Title = title;
        Text = text;
        Tags = tags;
    }
}
