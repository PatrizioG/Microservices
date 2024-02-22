
using FastEndpoints;
using FastEndpoints.Swagger;
using MassTransit;
using System.Reflection;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddFastEndpoints()
            .SwaggerDocument(o =>
            {
                o.ShortSchemaNames = true;
                o.DocumentSettings = s => s.DocumentName = "v1";
            });

        builder.Services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            // By default, sagas are in-memory, but should be changed to a durable
            // saga repository.
            x.SetInMemorySagaRepositoryProvider();

            var entryAssembly = Assembly.GetEntryAssembly();

            x.AddConsumers(entryAssembly);
            x.AddSagaStateMachines(entryAssembly);
            x.AddSagas(entryAssembly);
            x.AddActivities(entryAssembly);

            // In memory configuration
            //x.UsingInMemory((context, cfg) =>
            //{
            //    cfg.ConfigureEndpoints(context);
            //});

            // RabbitMQ configuration
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        var app = builder.Build();

        app.UseDefaultExceptionHandler()
            .UseFastEndpoints(x => x.Endpoints.RoutePrefix = "api");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
            app.UseSwaggerGen();

        app.Run();
    }
}
