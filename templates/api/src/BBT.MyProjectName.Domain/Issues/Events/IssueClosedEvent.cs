using BBT.Aether.Events;

namespace BBT.MyProjectName.Issues.Events;

/// <summary>
/// Event published when an issue is closed.
/// </summary>
[EventName(name: "IssueClosed", topic: "issue.closed")]
public class IssueClosedEvent
{
    /// <summary>
    /// The unique identifier of the closed issue.
    /// </summary>
    public Guid IssueId { get; set; }

    /// <summary>
    /// The repository ID where the issue belongs.
    /// </summary>
    public Guid RepositoryId { get; set; }

    /// <summary>
    /// The title of the issue.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The reason why the issue was closed.
    /// </summary>
    public IssueCloseReason? CloseReason { get; set; }

    /// <summary>
    /// The ID of the user who closed the issue (if available).
    /// </summary>
    public Guid? ClosedByUserId { get; set; }

    /// <summary>
    /// The timestamp when the issue was closed.
    /// </summary>
    public DateTime ClosedAt { get; set; }

    public IssueClosedEvent()
    {
    }

    public IssueClosedEvent(
        Guid issueId,
        Guid repositoryId,
        string title,
        IssueCloseReason? closeReason)
    {
        IssueId = issueId;
        RepositoryId = repositoryId;
        Title = title;
        CloseReason = closeReason;
    }
}
