using MassTransit;
using PubSubDto;

namespace Sub2;

public class Sub2Consumer : IConsumer<NotificationDto>
{
    public async Task Consume(ConsumeContext<NotificationDto> context)
    {
        Console.WriteLine($"Sub2 id: {context.Message.Id}, title: {context.Message.Title}, when: {context.Message.When}");
    }
}