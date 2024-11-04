using CCPDto;
using MassTransit;

namespace CCPSub2;

public class Sub2Consumer: IConsumer<OfferDto>
{
    public async Task Consume(ConsumeContext<OfferDto> context)
    {
        Console.WriteLine($"Sub2 name: {context.Message.Name}, price: {context.Message.Price}");
        await Task.Delay(5000);
    }
}