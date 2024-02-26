using Common.Contracts;
using Orders.Models;

namespace Orders.Mappers
{
    internal static class OrderMapper
    {
        public static Order MapOrder(OrderEntity orderEntity, List<Product> products, User user)
        {
            var lines = orderEntity.Lines.Select(line =>
            {
                return new OrderLine
                {
                    Id = line.Id,
                    ProductId = line.ProductId,
                    Price = line.Price,
                    Quantity = line.Quantity
                };

            }).ToList();

            return new Order
            {
                Id = orderEntity.Id,
                OrderDateUtc = orderEntity.OrderDateUtc,
                UserId = orderEntity.UserId,
                OrderLines = lines,
                Products = products,
                User = user
            };
        }
    }
}
