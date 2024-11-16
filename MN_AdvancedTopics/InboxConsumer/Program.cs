using InboxConsumer;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = CreateHostBuilder(args).Build();
host.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddDbContext<InboxDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=9002;Database=postgres;Username=postgres;Password=postgres;"));
            
            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<InboxDbContext>(o =>
                {
                    o.UsePostgres();

                    o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
                });
                
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumer<ExampleConsumer>();
                x.AddConfigureEndpointsCallback((context, name, cfg) =>
                {
                    cfg.UseEntityFrameworkOutbox<InboxDbContext>(context);
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
        });