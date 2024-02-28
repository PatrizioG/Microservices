using Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.Mappers;
using Orders.Models;

namespace Orders.Consumers;

public class UpdateOrderConsumer : IConsumer<UpdateOrder>
{
    private readonly OrdersDbContext _ordersContext;
    private readonly ILogger<UpdateOrderConsumer> _logger;

    public UpdateOrderConsumer(
        OrdersDbContext context,
        ILogger<UpdateOrderConsumer> logger)
    {
        _ordersContext = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UpdateOrder> context)
    {
        _logger.LogInformation("UpdateOrder consumed");

        var orderEntity = await _ordersContext.Orders
            .Include(p => p.Lines)
            .SingleOrDefaultAsync(p => p.Id.Equals(context.Message.OrderId));

        if (orderEntity == null)
            throw new ArgumentException("Order not found");

        // Update lines
        foreach (var line in context.Message.OrderLines)
        {
            var lineEntity = orderEntity.Lines.FirstOrDefault(l => l.Id.Equals(line.OrderLineId));
            if (lineEntity == null)
                continue;

            if (line.Quantity <= 0)
                orderEntity.Lines.Remove(lineEntity);
            else
                lineEntity.Quantity = line.Quantity;
        }

        var rowUpdated = await _ordersContext.SaveChangesAsync();
        await context.RespondAsync(new GenericResult{Message = $"Updated {rowUpdated} rows"});
    }
}