using MassTransit;

namespace TopicsSub1;

public class TopicsSub1 : IConsumer<TopicsDto.TopicsDto>
{
    public async Task Consume(ConsumeContext<TopicsDto.TopicsDto> context)
    {
        Console.WriteLine($"Sub1 message: {context.Message.Message}");
        await Task.Delay(5000);
    }
}