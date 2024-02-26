using Common.Contracts;
using MassTransit;
using Orders.Mappers;
using Orders.Models;

namespace Orders.Consumers
{
    public class CreateOrderConsumer : IConsumer<CreateOrder>
    {
        private readonly OrdersDbContext _ordersContext;
        private readonly ILogger<CreateOrderConsumer> _logger;
        private readonly IRequestClient<GetUserById> _userRequestClient;
        private readonly IRequestClient<GetProductById> _productRequestClient;

        public CreateOrderConsumer(
            OrdersDbContext context,
            ILogger<CreateOrderConsumer> logger,
            IRequestClient<Common.Contracts.GetUserById> userRequestClient,
            IRequestClient<Common.Contracts.GetProductById> productRequestClient)
        {
            _ordersContext = context;
            _logger = logger;
            _userRequestClient = userRequestClient;
            _productRequestClient = productRequestClient;
        }

        public async Task Consume(ConsumeContext<CreateOrder> context)
        {
            _logger.LogInformation("CreateOrder consumed");

            // Get user or throw
            Response<User> response = await _userRequestClient.GetResponse<Common.Contracts.User>(new Common.Contracts.GetUserById { Id = context.Message.UserId });
            User user = response.Message;

            List<(Product, double)> products = [];

            foreach (var orderLine in context.Message.OrderLines)
            {
                // Get product or throw
                var response2 = await _productRequestClient.GetResponse<Common.Contracts.Product>(new Common.Contracts.GetProductById { Id = orderLine.ProductId });
                products.Add((response2.Message, orderLine.Quantity));
            }

            var orderEntity = new OrderEntity()
            {
                Id = Guid.NewGuid().ToString(),
                OrderDateUtc = DateTime.UtcNow,
                UserId = user.Id,
                Lines = products.Select(tuple =>
                {
                    var (product, quantity) = tuple;

                    return new OrderLineEntity
                    {

                        Id = Guid.NewGuid().ToString(),
                        ProductId = product.Id,
                        Price = product.Price,
                        Quantity = quantity
                    };

                }).ToList()
            };

            await _ordersContext.Orders.AddAsync(orderEntity);
            await _ordersContext.SaveChangesAsync();

            await context.RespondAsync<Order>(OrderMapper.MapOrder(orderEntity, products.Select(x => x.Item1).ToList(), user));
        }
    }
}
