using Common.Services;
using Microsoft.EntityFrameworkCore;
using Users.Models;

namespace Users;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Seed default users
        var scope = host.Services.CreateAsyncScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        await userContext.Users.AddRangeAsync(
            //Generated with https://www.behindthename.com/
            new UserEntity { Id = "2E7B29DA-1115-4073-A6C0-63BC19370977", Name = "Talaat", Surname = "Mehrab", Email = "Talaat.Mehrab@gmail.com" },
            new UserEntity { Id = "264B8DF2-AE52-45C8-AB79-3AE2A7FA4DD0", Name = "Yaqoob", Surname = "Krol", Email = "Yaqoob.Krol@yahoo.com" },
            new UserEntity { Id = "14DB2B36-AAFF-4999-A639-4051A578A19E", Name = "Setareh ", Surname = "Jolene", Email = "Setareh.Jolene@gmail.com" },
            new UserEntity { Id = "7B5CC1E2-FA31-493A-A73C-E5EE68F0E99E", Name = "Kyriakos", Surname = "Sawyer", Email = "Kyriakos.Sawyer@yahoo.com" },
            new UserEntity { Id = "1D2F81BD-AC8C-41FC-9F88-3C9AE26EB88B", Name = "Inta", Surname = "Khan", Email = "Inta.Khan@outlook.com" }
        );
        await userContext.SaveChangesAsync();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .AddDefaultMassTransit()
                    .AddDbContext<UsersDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("UsersDb");
                    });
            });
}
