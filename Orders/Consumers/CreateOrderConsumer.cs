using Common.Contracts;
using MassTransit;
using Orders.Models;

namespace Orders.Consumers
{
    public class CreateOrderConsumer : IConsumer<CreateOrder>
    {
        private readonly OrdersDbContext ordersContext;
        private readonly ILogger<CreateOrderConsumer> _logger;
        private readonly IRequestClient<GetUserById> _userRequestClient;
        private readonly IRequestClient<GetProductById> _productRequestClient;

        public CreateOrderConsumer(
            OrdersDbContext context,
            ILogger<CreateOrderConsumer> logger,
            IRequestClient<Common.Contracts.GetUserById> userRequestClient,
            IRequestClient<Common.Contracts.GetProductById> productRequestClient)
        {
            ordersContext = context;
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

            List<Product> products = [];

            foreach (var productId in context.Message.ProductIds)
            {
                // Get product or throw
                var response2 = await _productRequestClient.GetResponse<Common.Contracts.Product>(new Common.Contracts.GetProductById { Id = productId });
                products.Add(response2.Message);
            }

            var orderEntity = new OrderEntity()
            {
                Id = Guid.NewGuid().ToString(),
                OrderDateUtc = DateTime.UtcNow,
                UserId = user.Id,
                Lines = products.Select(x => new OrderLineEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = x.Id,
                    Price = x.Price,

                }).ToList()
            };

            await ordersContext.Orders.AddAsync(orderEntity);
            await ordersContext.SaveChangesAsync();

            var result = new CreateOrderResult();
            await context.RespondAsync<CreateOrderResult>(result);
        }
    }
}
