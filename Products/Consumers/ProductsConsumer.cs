namespace Company.Consumers;

using Contracts;
using MassTransit;
using Products.Models;
using System.Threading.Tasks;

internal class ProductsConsumer : IConsumer<GetAllProducts>
{
    private readonly ProductsContext _context;

    public ProductsConsumer(ProductsContext context)
    {
        _context = context;
    }

    public Task Consume(ConsumeContext<GetAllProducts> context)
    {
        _context.Products
        return Task.CompletedTask;
    }
}