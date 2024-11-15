using Choreographer.Models;
using MassTransit;

namespace Choreographer.Consumers;

public class FlightBookingConsumer : IConsumer<BookFlight>
{
    public async Task Consume(ConsumeContext<BookFlight> context)
    {
        Console.WriteLine($"Booking flight for order {context.Message.OrderId}");
        await context.Publish(new FlightBooked(context.Message.OrderId));
    }
}