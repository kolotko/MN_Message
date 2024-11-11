using MassTransit;
using Microsoft.Extensions.Hosting;

namespace RequestReplyPub;

public class Pub(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            Console.ReadKey(true);
            var requestClient = bus.CreateRequestClient<RequestReplyDto.RequestReplyDto>();
            var response = await requestClient.GetResponse<RequestReplyDto.Response1Dto, RequestReplyDto.Response2Dto>(new RequestReplyDto.RequestReplyDto { Message = "Przykładowy request"}, stoppingToken);
            if (response.Is(out Response<RequestReplyDto.Response1Dto> response1))
            {
                Console.WriteLine($"{response1.Message.Message1}");
            }
            if (response.Is(out Response<RequestReplyDto.Response2Dto> response2))
            {
                Console.WriteLine($"{response2.Message.Status}");
            }
        }
    }
}