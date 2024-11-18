using DeadLetter;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = CreateHostBuilder(args).Build();
host.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<DeadLetterConsumer>();
                x.AddConsumer<FaultConsumer>();
                x.AddConfigureEndpointsCallback((context,name,cfg) =>
                {
                    //jeśli plugin dodany w rabbitMq
                    // cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(60)));
                    cfg.UseMessageRetry(r => r.Immediate(3));
                });
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", 9000, "/", h =>
                    {
                        h.Username("user");
                        h.Password("password");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddHostedService<Publish>();
        });