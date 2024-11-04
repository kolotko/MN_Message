using MassTransit;
using Microsoft.Extensions.Hosting;
using PubSubDto;

namespace Pub;

public class NotificationPublisher(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            Console.ReadKey(true);
            await bus.Publish(new NotificationDto(Guid.NewGuid(), "Example title", DateTime.Now), stoppingToken);
            await Task.Delay(200, stoppingToken);
        }
    }
}