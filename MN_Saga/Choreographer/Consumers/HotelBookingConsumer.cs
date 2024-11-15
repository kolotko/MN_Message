using Choreographer.Models;
using MassTransit;

namespace Choreographer.Consumers;

public class HotelBookingConsumer : IConsumer<FlightBooked>
{
    public Task Consume(ConsumeContext<FlightBooked> context)
    {
        Console.WriteLine($"Booking hotel for order {context.Message.OrderId}");
        return Task.CompletedTask;
    }
}