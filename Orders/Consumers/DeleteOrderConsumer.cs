using Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.Models;

namespace Orders.Consumers;

public class DeleteOrderConsumer : IConsumer<DeleteOrder>
{
    private readonly OrdersDbContext _ordersContext;
    private readonly ILogger<DeleteOrderConsumer> _logger;

    public DeleteOrderConsumer(OrdersDbContext context, ILogger<DeleteOrderConsumer> logger)
    {
        _ordersContext = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DeleteOrder> context)
    {
        _logger.LogInformation("DeleteOrder consumed");

        var order = await _ordersContext.Orders
            .Include(p => p.Lines)
            .SingleOrDefaultAsync(p => p.Id.Equals(context.Message.Id));

        if (order == null)
            throw new ArgumentException("Order not found");

        _ordersContext.Remove(order);
        await _ordersContext.SaveChangesAsync();

        await context.RespondAsync(new GenericResult { Message = "Order deleted" });
    }
}
