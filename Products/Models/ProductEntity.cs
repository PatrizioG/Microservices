namespace Products.Models;

public class ProductEntity
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string CategoryId { get; set; } = string.Empty;
    public CategoryEntity Category { get; set; } = new();
}