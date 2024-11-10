using MassTransit;
using Microsoft.Extensions.Hosting;
using RoutingDto;

namespace RoutingPub;

public class LogPublisher(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            Console.ReadKey(true);
            await bus.Publish<LogDto>(new
            {
                Message = "test"
            }, x =>
            {
                x.SetRoutingKey("B");
            }, stoppingToken);
            await Task.Delay(200, stoppingToken);
        }
    }
}