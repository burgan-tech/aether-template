using BBT.Aether.Aspects;
using BBT.Aether.Events;
using BBT.MyProjectName.Issues.Events;
using Microsoft.Extensions.Logging;

namespace BBT.MyProjectName.Issues.Handlers;

/// <summary>
/// Event handler for IssueClosedEvent.
/// This handler demonstrates how to subscribe to and handle IssueClosedEvent published via EventBus.
/// </summary>
public class IssueClosedEventHandler(ILogger<IssueClosedEventHandler> logger) : IEventHandler<IssueClosedEvent>
{
    /// <summary>
    /// Handles the IssueClosedEvent when it is published via EventBus.
    /// </summary>
    /// <param name="eventData">The IssueClosedEvent data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task HandleAsync(CloudEventEnvelope<IssueClosedEvent> eventData, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "IssueClosedEvent received - IssueId: {IssueId}, RepositoryId: {RepositoryId}, Title: {Title}, CloseReason: {CloseReason}, ClosedAt: {ClosedAt}",
            eventData.Data.IssueId,
            eventData.Data.RepositoryId,
            eventData.Data.Title,
            eventData.Data.CloseReason,
            eventData.Time);

        // Example: You can add additional business logic here such as:
        // - Sending notifications about issue closure
        // - Updating analytics/metrics for closed issues
        // - Cleaning up related resources
        // - Triggering completion workflows
        // - Archiving related data
        // - etc.

        // Simulate async work (e.g., external API calls, database operations)
        await Task.CompletedTask;

        logger.LogInformation(
            "IssueClosedEvent processed successfully for Issue {IssueId} with reason {CloseReason}",
            eventData.Data.IssueId,
            eventData.Data.CloseReason);
    }
}
