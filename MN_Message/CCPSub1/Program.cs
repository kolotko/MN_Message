﻿using CCPSub1;
using MassTransit;
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
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", 9000, "/", h =>
                    {
                        h.Username("user");
                        h.Password("password");
                    });
                    
                    cfg.ReceiveEndpoint("offer", e =>
                    {
                        e.Consumer<Sub1Consumer>();
                    });
                });
            });
        });