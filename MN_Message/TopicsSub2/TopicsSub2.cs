using MassTransit;

namespace TopicsSub2;

public class TopicsSub2 : IConsumer<TopicsDto.TopicsDto>
{
    public async Task Consume(ConsumeContext<TopicsDto.TopicsDto> context)
    {
        Console.WriteLine($"Sub2 message: {context.Message.Message}");
        await Task.Delay(5000);
    }
}