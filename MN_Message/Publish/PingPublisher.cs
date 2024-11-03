using Dto;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Publish;

public class PingPublisher(IBus _bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            var keyPressed = Console.ReadKey(true);
            await _bus.Publish(new PingTest(keyPressed.Key.ToString()), stoppingToken);
            await Task.Delay(200, stoppingToken);
        }
    }
}