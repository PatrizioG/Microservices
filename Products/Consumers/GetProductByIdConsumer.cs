using Common.Contracts;
using MassTransit;
using Products.Models;

namespace Products.Consumers;

public class GetProductByIdConsumer : IConsumer<GetProductById>
{
    private readonly ProductsContext _context;
    private readonly ILogger<GetProductByIdConsumer> _logger;

    public GetProductByIdConsumer(ProductsContext context, ILogger<GetProductByIdConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetProductById> context)
    {
        _logger.LogInformation("GetProductById consumed");
        await context.RespondAsync<ProductByIdResult>(new ProductByIdResult { Id = "666", Name = "Prodotto666", Category = "Ferramenta" });
    }
}
