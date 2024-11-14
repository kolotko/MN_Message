using MassTransit;
using Orchestrator.Events;

namespace Orchestrator.Handlers;

public class EventHandler2 : IConsumer<Event2>
{
    public Task Consume(ConsumeContext<Event2> context)
    {
        Console.WriteLine($"Event2 received: {context.Message.Email}");
        return Task.CompletedTask;
    }
}