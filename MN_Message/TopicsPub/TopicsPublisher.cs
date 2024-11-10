using MassTransit;
using Microsoft.Extensions.Hosting;

namespace TopicsPub;

public class TopicsPublisher(IBus bus): BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            Console.ReadKey(true);
            await bus.Publish<TopicsDto.TopicsDto>(new
            {
                Message = "test"
            }, x =>
            {
                x.SetRoutingKey("topic.test.routing");
            }, stoppingToken);
            await Task.Delay(200, stoppingToken);
        }
    }
}