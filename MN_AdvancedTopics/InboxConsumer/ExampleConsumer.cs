using MassTransit;
using OutboxDto;

namespace InboxConsumer;

public class ExampleConsumer : IConsumer<TestMessage>
{
    public async Task Consume(ConsumeContext<TestMessage> context)
    {
        Console.WriteLine($"Received message: {context.Message.Content}");
        await Task.CompletedTask;
    }
}