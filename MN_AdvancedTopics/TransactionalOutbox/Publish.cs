using MassTransit;
using Microsoft.Extensions.Hosting;
using OutboxDto;

namespace TransactionalOutbox;

public class Publish(OutboxDbContext _context, IPublishEndpoint _publishEndpoint) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            Console.ReadKey(true);
            var message = new TestMessage()
            {
                Content = "hej"
            };
            await _context.TestMessage.AddAsync(message);
            await _publishEndpoint.Publish(message, stoppingToken);
            await _context.SaveChangesAsync(stoppingToken);
        }
    }
}