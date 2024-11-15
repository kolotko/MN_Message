using Choreographer.Models;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Choreographer;

public class Publish(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            Console.ReadKey(true);
            await bus.Publish(new BookFlight(Guid.NewGuid()), stoppingToken);
        }
    }
}