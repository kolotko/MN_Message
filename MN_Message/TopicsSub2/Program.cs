using MassTransit;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

var host = CreateHostBuilder(args).Build();
host.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers(typeof(Program).Assembly);
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", 9000, "/", h =>
                    {
                        h.Username("user");
                        h.Password("password");
                    });

                    cfg.ReceiveEndpoint("topic-b", e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.Lazy = true;
                        e.PrefetchCount = 20;
                        e.Bind<TopicsDto.TopicsDto>( z =>
                        {
                            z.ExchangeType = ExchangeType.Topic;
                            z.RoutingKey = "topic.*.routing";
                        });
                        e.Consumer<TopicsSub2.TopicsSub2>();
                    });
                });
            });
        });