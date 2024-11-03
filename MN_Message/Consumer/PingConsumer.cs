using Dto;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumer;

public class PingConsumer(ILogger<PingConsumer> _logger) : IConsumer<PingTest>
{
    public Task Consume(ConsumeContext<PingTest> context)
    {
        _logger.LogInformation(context.Message.Message);
        return Task.CompletedTask;
    }
}