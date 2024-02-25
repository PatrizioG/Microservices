using Products.Models;

namespace Products.Mappers
{
    internal static class ProductMapper
    {
        public static Common.Contracts.Product MapProduct(ProductEntity productEntity)
        {
            Common.Contracts.Product product = new()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Price = productEntity.Price,
            };

            if (productEntity.Category != null)
            {
                Common.Contracts.Category category = new()
                {
                    Id = productEntity.Category.Id,
                    Name = productEntity.Category.Name,
                };

                if (productEntity.Category.Father != null)
                {
                    Common.Contracts.Category father = new()
                    {
                        Id = productEntity.Category.Father.Id,
                        Name = productEntity.Category.Father.Name,
                    };

                    category.Father = father;
                }

                product.Category = category;
            }

            return product;
        }
    }
}
