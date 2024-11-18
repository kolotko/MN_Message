using MassTransit;

namespace DeadLetter;

public class FaultConsumer : IConsumer<Fault<TestDto>>
{
    public Task Consume(ConsumeContext<Fault<TestDto>> context)
    {
        Console.WriteLine("Błąd odebrany");
        return Task.CompletedTask;
    }
}