namespace Products.Consumers;

using Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Products.Mappers;
using Products.Models;
using System.Threading.Tasks;

public class GetAllProductsConsumer : IConsumer<GetAllProducts>
{
    private readonly ProductsDbContext _productsContext;
    private readonly ILogger<GetAllProductsConsumer> _logger;

    public GetAllProductsConsumer(ProductsDbContext context, ILogger<GetAllProductsConsumer> logger)
    {
        _productsContext = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetAllProducts> context)
    {
        _logger.LogInformation("GetAllProducts consumed");

        var products = await _productsContext.Products
            .Include(p => p.Category)
            .ThenInclude(c => c.Father) // Only 2 level of eager loading
            .ToListAsync();

        var getAllProductsResult = new GetAllProductsResult();

        foreach (var productEntity in products)
            getAllProductsResult.Products.Add(ProductMapper.MapProduct(productEntity));

        await context.RespondAsync<GetAllProductsResult>(getAllProductsResult);
    }
}