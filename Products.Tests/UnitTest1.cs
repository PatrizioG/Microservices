using Common.Contracts;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Consumers;
using Products.Models;

namespace Products.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x => { x.AddConsumer<GetAllProductsConsumer>(); })
            .AddDbContext<ProductsDbContext>(options => options.UseInMemoryDatabase("ProductsDbTest"))
            .BuildServiceProvider(true);

        var scope = provider.CreateAsyncScope();
        var productsDbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();

        var category = new CategoryEntity { Id = "1", Name = "Category" };
        var subCategory = new CategoryEntity { Id = "2", Name = "Sub category", Father = category };
        await productsDbContext.Products.AddRangeAsync(
            new ProductEntity { Id = "1", Category = subCategory, Name = "Product 1" },
            new ProductEntity { Id = "2", Category = subCategory, Name = "Product 2" },
            new ProductEntity { Id = "3", Category = subCategory, Name = "Product 3" });
        await productsDbContext.SaveChangesAsync();

        var harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        var client = harness.GetRequestClient<GetAllProducts>();

        var response = await client.GetResponse<GetAllProductsResult>(new { });
        Assert.NotEmpty(response.Message.Products);
        Assert.Equal("Product 1", response.Message.Products.First().Name);
    }
}