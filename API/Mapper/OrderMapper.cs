namespace API.Mapper;

internal static class OrderMapper
{
    public static CommonDtos.OrderDto MapOrder(Common.Contracts.Order order)
    {
        return new CommonDtos.OrderDto
        {
            OrderId = order.Id,
            OrderDateUtc = order.OrderDateUtc,
            UserName = order.User.Name,
            UserSurname = order.User.Surname,
            UserEmail = order.User.Email,
            OrderLines = order.OrderLines.Select(line =>
            {
                var product = order.Products.FirstOrDefault(p => p.Id.Equals(line.ProductId))
                ?? throw new ArgumentException($"Product {line.ProductId} not found");

                var productDto = ProductMapper.MapProduct(product);
                return new CommonDtos.OrderLineDto
                {
                    ProductName = order.Products.First(p => p.Id.Equals(line.ProductId)).Name,
                    Price = line.Price,
                    Quantity = line.Quantity,
                    ProductCategory = productDto.Category
                };

            }).ToList()
        };
    }
}
