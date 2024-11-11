using MassTransit;

namespace RequestReplySub1;

public class Sub1 : IConsumer<RequestReplyDto.RequestReplyDto>
{
    public async Task Consume(ConsumeContext<RequestReplyDto.RequestReplyDto> context)
    {
        Console.WriteLine($"Wiadomośc odebrana");
        await context.RespondAsync(new RequestReplyDto.Response1Dto()
        {
            Message1 = "odpowiedź 1"
        });
        // await context.RespondAsync(new RequestReplyDto.Response2Dto()
        // {
        //     Status = 5
        // });
    }
}