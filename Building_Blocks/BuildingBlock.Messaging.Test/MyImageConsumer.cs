using MassTransit;

namespace BuildingBlock.Messaging.Test;

public class MyImageConsumer : IConsumer<SaveImageCommandRequest>
{
    public bool HasProcessedMessage { get; private set; } = false;

    public Task Consume(ConsumeContext<SaveImageCommandRequest> context)
    {
        // Logica per consumare il messaggio
        Console.WriteLine($"Received image: {context.Message.FileName}");
        HasProcessedMessage = true;

        return Task.CompletedTask;
    }
}
