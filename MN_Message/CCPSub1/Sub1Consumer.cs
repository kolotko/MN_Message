using CCPDto;
using MassTransit;

namespace CCPSub1;

public class Sub1Consumer: IConsumer<OfferDto>
{
    public async Task Consume(ConsumeContext<OfferDto> context)
    {
        Console.WriteLine($"Sub1 name: {context.Message.Name}, price: {context.Message.Price}");
        await Task.Delay(5000);
    }
}