using BBT.Aether.Events;

namespace BBT.MyProjectName.Backgrounds;

/// <summary>
/// Background service that processes outbox messages.
/// This service wraps the OutboxProcessor to process outbox messages in the background.
/// </summary>
internal sealed class OutboxBackgroundService: BackgroundService
{
    private readonly IOutboxProcessor _processor;

    public OutboxBackgroundService(IOutboxProcessor processor)
    {
        _processor = processor;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _processor.RunAsync(stoppingToken);
    }
}