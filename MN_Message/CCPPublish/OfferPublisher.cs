using CCPDto;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace CCPPublish;

public class OfferPublisher(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            Console.ReadKey(true);
            var endpoint = await bus.GetSendEndpoint(new Uri("queue:offer"));
            await endpoint.Send(new OfferDto("Test", 50), stoppingToken);
            await Task.Delay(200, stoppingToken);
        }
    }
}