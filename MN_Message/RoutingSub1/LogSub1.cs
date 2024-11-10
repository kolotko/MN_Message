using MassTransit;
using RoutingDto;

namespace RoutingSub1;

public class LogSub1 : IConsumer<LogDto>
{
    public async Task Consume(ConsumeContext<LogDto> context)
    {
        Console.WriteLine($"Sub1 message: , {context.ReceiveContext.InputAddress}");
        await Task.Delay(5000);
    }
}