namespace Products.Consumers;

using Common.Contracts;
using MassTransit;
using Products.Models;
using System.Threading.Tasks;

public class GetAllProductsConsumer : IConsumer<GetAllProducts>
{
    private readonly ProductsContext _context;
    private readonly ILogger<GetAllProductsConsumer> _logger;

    public GetAllProductsConsumer(ProductsContext context, ILogger<GetAllProductsConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetAllProducts> context)
    {
        _logger.LogInformation("GetAllProducts consumed");
        await context.RespondAsync<AllProductsResult>(new AllProductsResult { ProductId = "666" });
    }
}