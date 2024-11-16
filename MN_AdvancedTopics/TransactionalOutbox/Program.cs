using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TransactionalOutbox;

var host = CreateHostBuilder(args).Build();
host.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<OutboxDbContext>(p =>
                {
                    p.QueryDelay = TimeSpan.FromSeconds(1);
                    p.UsePostgres().UseBusOutbox();
                    p.DisableInboxCleanupService(); // w tym projekcie wypychamy, a nie konsumujemy wiadomości
                });     
                x.SetKebabCaseEndpointNameFormatter();
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", 9000, "/", h =>
                    {
                        h.Username("user");
                        h.Password("password");
                    });
                });
            });
            services.AddDbContext<OutboxDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=9002;Database=postgres;Username=postgres;Password=postgres;"));
            services.AddHostedService<Publish>();
        });