using MassTransit;
using Orchestrator.Events;

namespace Orchestrator.Handlers;

public class EventHandler1 : IConsumer<Event1>
{
    public Task Consume(ConsumeContext<Event1> context)
    {
        Console.WriteLine($"Event1 received: {context.Message.Email}");
        return Task.CompletedTask;
    }
}