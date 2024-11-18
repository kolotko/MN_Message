using MassTransit;

namespace DeadLetter;

public class DeadLetterConsumer: IConsumer<TestDto>
{
    public Task Consume(ConsumeContext<TestDto> context)
    {
        throw new Exception("Very bad things happened");
    }
}