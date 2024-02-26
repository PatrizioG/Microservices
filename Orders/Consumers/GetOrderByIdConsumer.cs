using Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.Mappers;
using Orders.Models;

namespace Orders.Consumers
{
    public class GetOrderByIdConsumer : IConsumer<GetOrderById>
    {
        private readonly OrdersDbContext _ordersContext;
        private readonly ILogger<GetOrderByIdConsumer> _logger;
        private readonly IRequestClient<GetUserById> _userRequestClient;
        private readonly IRequestClient<GetProductById> _productRequestClient;

        public GetOrderByIdConsumer(
            OrdersDbContext context,
            ILogger<GetOrderByIdConsumer> logger,
            IRequestClient<Common.Contracts.GetUserById> userRequestClient,
            IRequestClient<Common.Contracts.GetProductById> productRequestClient)
        {
            _ordersContext = context;
            _logger = logger;
            _userRequestClient = userRequestClient;
            _productRequestClient = productRequestClient;
        }

        public async Task Consume(ConsumeContext<GetOrderById> context)
        {
            _logger.LogInformation("GetOrderById consumed");

            var order = await _ordersContext.Orders
                .Include(p => p.Lines)
                .SingleOrDefaultAsync(p => p.Id.Equals(context.Message.Id));

            if (order == null)
                throw new ArgumentException("Order not found");

            // Get user or throw
            Response<User> response = await _userRequestClient.GetResponse<Common.Contracts.User>(new Common.Contracts.GetUserById { Id = order.UserId });
            User user = response.Message;

            List<Product> products = [];

            foreach (var orderLine in order.Lines)
            {
                // Get product or throw
                var response2 = await _productRequestClient.GetResponse<Common.Contracts.Product>(new Common.Contracts.GetProductById { Id = orderLine.ProductId });
                products.Add(response2.Message);
            }

            await context.RespondAsync<Common.Contracts.Order>(OrderMapper.MapOrder(order, products, user));
        }
    }
}
