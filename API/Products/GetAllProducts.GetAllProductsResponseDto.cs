using API.CommonDtos;

namespace API.Products;

public class GetAllProductsResponseDto
{
    public List<ProductDto> Products { get; set; } = [];
}