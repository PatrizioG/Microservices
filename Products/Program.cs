using Common.Services;
using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var scope = host.Services.CreateAsyncScope();
        var productsDbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();

        // Seed default categories
        var abbigliamento = new CategoryEntity { Id = "B0B2B710-C91E-4F72-8A56-B3A760D3C34A", Name = "Abbigliamento" };
        var uomo = new CategoryEntity { Id = "3830B4D1-D963-49C9-A26B-0C167F6A2443", Name = "Abbigliamento uomo", Father = abbigliamento };
        var donna = new CategoryEntity { Id = "AFE8304E-DC96-4611-BEB7-B6C424AD7F9F", Name = "Abbigliamento donna", Father = abbigliamento };
        var calzature = new CategoryEntity { Id = "BCB811C6-7CF6-45F5-A338-1252C2E3682C", Name = "Calzature" };

        // Seed default products
        await productsDbContext.Products.AddRangeAsync(
              new ProductEntity { Id = "4761414B-6398-4DB9-9FC4-BA542A1D2A04", Category = uomo, Name = "T-shirt", Price = 19.90 },
              new ProductEntity { Id = "6A79D62F-2D37-4CBE-A289-F539E4ADBB0D", Category = uomo, Name = "Giacca", Price = 235.00 },
              new ProductEntity { Id = "F58A2DD9-781B-4ABA-AE79-6EF0AD4FBA70", Category = uomo, Name = "Pantalone", Price = 79.90 },
              new ProductEntity { Id = "14C2C1BD-C409-42F7-89DA-C7DBBDE8B25A", Category = donna, Name = "Gonna", Price = 27.70 },
              new ProductEntity { Id = "8C981510-E124-4FFE-935D-51EE926F103C", Category = donna, Name = "Camicetta", Price = 45.00 },
              new ProductEntity { Id = "4901CCF5-537D-412B-9604-9E2C47E5E93A", Category = calzature, Name = "Scarponcini", Price = 89.99 },
              new ProductEntity { Id = "77253273-B286-46F9-A6D9-FE038781D2F7", Category = calzature, Name = "Stivali", Price = 115.99 }
          );

        await productsDbContext.SaveChangesAsync();

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services
                   .AddDefaultMassTransit()
                   .AddDbContext<ProductsDbContext>(options =>
                   {
                       options
                        .UseInMemoryDatabase("ProductsDb")
                        .EnableSensitiveDataLogging(true);
                   });
            });
}
