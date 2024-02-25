using Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Products.Mappers;
using Products.Models;

namespace Products.Consumers;

public class GetProductByIdConsumer : IConsumer<GetProductById>
{
    private readonly ProductsDbContext _context;
    private readonly ILogger<GetProductByIdConsumer> _logger;

    public GetProductByIdConsumer(ProductsDbContext context, ILogger<GetProductByIdConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetProductById> context)
    {
        _logger.LogInformation("GetProductById consumed");

        var product = await _context.Products
            .Include(p => p.Category)
            .ThenInclude(c => c.Father)
            .SingleOrDefaultAsync(p => p.Id.Equals(context.Message.Id));

        if (product == null)
            throw new ArgumentException("Product not found");

        await context.RespondAsync<Common.Contracts.Product>(ProductMapper.MapProduct(product));
    }
}
