using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orchestrator;
using Orchestrator.Database;
using Orchestrator.Saga;

var host = CreateHostBuilder(args).Build();
host.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=9002;Database=postgres;Username=postgres;Password=postgres;"));
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers(typeof(Program).Assembly);
                x.AddSagaStateMachine<EventSaga, EventSagaData>()
                    .EntityFrameworkRepository(r =>
                    {
                        r.ExistingDbContext<AppDbContext>();
            
                        r.UsePostgres();
                    });
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", 9000, "/", h =>
                    {
                        h.Username("user");
                        h.Password("password");
                    });
                    cfg.UseInMemoryOutbox(context); // bez tego nie działa
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddHostedService<Publish>();
        });