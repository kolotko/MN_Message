using MassTransit;
using RoutingDto;

namespace RoutingSub2;

public class LogSub2: IConsumer<LogDto>
{
    public async Task Consume(ConsumeContext<LogDto> context)
    {
        Console.WriteLine($"Sub2 message: {context.Message.Message}");
        await Task.Delay(5000);
    }
}