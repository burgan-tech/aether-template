using BBT.Aether.Events;

namespace BBT.MyProjectName.Backgrounds;

/// <summary>
/// Background service that processes inbox messages.
/// This service wraps the OutboxProcessor to process outbox messages in the background.
/// </summary>
internal sealed class InboxBackgroundService : BackgroundService
{
    private readonly IInboxProcessor _processor;

    public InboxBackgroundService(IInboxProcessor processor)
    {
        _processor = processor;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _processor.RunAsync(stoppingToken);
    }
}