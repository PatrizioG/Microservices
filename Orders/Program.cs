using Common.Services;
using Microsoft.EntityFrameworkCore;
using Orders.Models;

namespace Orders;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .AddDefaultMassTransit()
                    .AddDbContext<OrdersDbContext>(options =>
                    {
                        options.UseSqlite("Data Source=Orders.db");
                    });
            });
}