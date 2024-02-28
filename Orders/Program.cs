using Common.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.Models;

namespace Orders;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var scope = host.Services.CreateAsyncScope();
        var productsDbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
        productsDbContext.Database.EnsureCreated();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .AddDefaultMassTransit()
                    .AddDbContext<OrdersDbContext>(options => { options.UseSqlite("Data Source=Orders.db"); });
            });
}