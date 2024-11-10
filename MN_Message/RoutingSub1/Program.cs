using MassTransit;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RoutingDto;
using RoutingSub1;

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

                    cfg.ReceiveEndpoint("route-a", e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.Lazy = true;
                        e.PrefetchCount = 20;
                        e.Bind<LogDto>( z =>
                        {
                            z.ExchangeType = ExchangeType.Direct;
                            z.RoutingKey = "A";
                        });
                        e.Consumer<LogSub1>();
                    });
                });
            });
        });