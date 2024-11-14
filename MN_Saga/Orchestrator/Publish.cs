using MassTransit;
using Microsoft.Extensions.Hosting;
using Orchestrator.Events;

namespace Orchestrator;

public class Publish(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            Console.ReadKey(true);
            await bus.Publish(new Event1()
            {
                EventId = Guid.NewGuid(),
                Email = "test@gmail.com",
            }, stoppingToken);
        }
    }
}