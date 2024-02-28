using Common.Contracts;

namespace API.Mapper;

internal static class ProductMapper
{
    public static CommonDtos.ProductDto MapProduct(Common.Contracts.Product product)
    {
        return new CommonDtos.ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Category = StringifyCategory(product.Category)
        };
    }

    private static string StringifyCategory(Category? category)
    {
        if (category == null)
            return string.Empty;

        if (category.Father == null)
            return category.Name;

        return $"{StringifyCategory(category.Father)} > {category.Name}";
    }
}