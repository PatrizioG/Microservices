
using Common.Services;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddFastEndpoints()
            .AddDefaultMassTransit()
            .SwaggerDocument(o =>
            {
                o.ShortSchemaNames = true;
                o.DocumentSettings = s => s.DocumentName = "v1";
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
