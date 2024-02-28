namespace API.CommonDtos;

public class ProductDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Category { get; set; } = string.Empty;
}