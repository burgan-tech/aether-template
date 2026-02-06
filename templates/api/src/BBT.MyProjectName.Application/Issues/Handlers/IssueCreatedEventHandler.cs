using BBT.Aether.Events;
using BBT.MyProjectName.Issues.Events;
using Microsoft.Extensions.Logging;

namespace BBT.MyProjectName.Issues.Handlers;

/// <summary>
/// Event handler for IssueCreatedEvent.
/// This handler demonstrates how to subscribe to and handle IssueCreatedEvent published via EventBus.
/// </summary>
public class IssueCreatedEventHandler(ILogger<IssueCreatedEventHandler> logger) : IEventHandler<IssueCreatedEvent>
{
    /// <summary>
    /// Handles the IssueCreatedEvent when it is published via EventBus.
    /// </summary>
    /// <param name="eventData">The IssueCreatedEvent data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task HandleAsync(CloudEventEnvelope<IssueCreatedEvent> eventData, CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation(
                "IssueCreatedEvent received - IssueId: {IssueId}, RepositoryId: {RepositoryId}, Title: {Title}, CreatedAt: {CreatedAt}",
                eventData.Data.IssueId,
                eventData.Data.RepositoryId,
                eventData.Data.Title,
                eventData.Time);

            // Example: You can add additional business logic here such as:
            // - Sending notifications to users
            // - Updating analytics/metrics
            // - Triggering other workflows
            // - Updating search indexes
            // - etc.

            // Simulate async work
            await Task.CompletedTask;

            logger.LogInformation("IssueCreatedEvent processed successfully for Issue {IssueId}",
                eventData.Data.IssueId);
        }
        catch (Exception e)
        {
            await Task.CompletedTask;
        }
    }
}
