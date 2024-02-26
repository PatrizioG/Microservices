using Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.Mappers;
using Orders.Models;

namespace Orders.Consumers;

public class GetAllOrdersConsumer : IConsumer<GetAllOrders>
{
    private readonly OrdersDbContext _ordersContext;
    private readonly ILogger<GetAllOrdersConsumer> _logger;
    private readonly IRequestClient<GetUserById> _userRequestClient;
    private readonly IRequestClient<GetProductById> _productRequestClient;

    public GetAllOrdersConsumer(OrdersDbContext context,
        ILogger<GetAllOrdersConsumer> logger,
        IRequestClient<Common.Contracts.GetUserById> userRequestClient,
        IRequestClient<Common.Contracts.GetProductById> productRequestClient)
    {
        _ordersContext = context;
        _logger = logger;
        _userRequestClient = userRequestClient;
        _productRequestClient = productRequestClient;
    }

    public async Task Consume(ConsumeContext<GetAllOrders> context)
    {
        _logger.LogInformation("GetAllOrders consumed");

        var orders = await _ordersContext.Orders
            .Include(p => p.Lines)
            .ToListAsync();

        if (orders == null)
            throw new ArgumentException("Orders not found");

        if (orders.Count == 0)
            throw new ArgumentException("Orders not found");

        var result = new Common.Contracts.OrdersResult();

        var userCache = new Dictionary<string, User>();
        var productCache = new Dictionary<string, Product>();

        foreach (var order in orders)
        {
            if (!userCache.TryGetValue(order.UserId, out User? user))
            {
                // Get user or throw
                Response<User> response = await _userRequestClient.GetResponse<Common.Contracts.User>(new Common.Contracts.GetUserById { Id = order.UserId });
                user = response.Message;
                userCache.Add(order.UserId, user);
            }

            List<Product> products = [];
            foreach (var orderLine in order.Lines)
            {
                if (!productCache.TryGetValue(orderLine.ProductId, out Product? product))
                {
                    // Get product or throw
                    var response2 = await _productRequestClient.GetResponse<Common.Contracts.Product>(new Common.Contracts.GetProductById { Id = orderLine.ProductId });
                    product = response2.Message;
                    productCache.Add(orderLine.ProductId, product);
                }

                products.Add(product);
            }

            result.Orders.Add(OrderMapper.MapOrder(order, products, user));
        }

        await context.RespondAsync<Common.Contracts.OrdersResult>(result);
    }
}
