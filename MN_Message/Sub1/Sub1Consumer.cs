using MassTransit;
using PubSubDto;

namespace Sub1;

public class Sub1Consumer : IConsumer<NotificationDto>
{
    public async Task Consume(ConsumeContext<NotificationDto> context)
    {
        Console.WriteLine($"Sub1 id: {context.Message.Id}, title: {context.Message.Title}, when: {context.Message.When}");
    }
}