namespace Products.Models;

public class CategoryEntity
{
    public string Id { get; set; } = string.Empty;
    public string? FatherId { get; set; }
    public CategoryEntity? Father { get; set; } = null;
    public string Name { get; set; } = string.Empty;
    public ICollection<ProductEntity> Products { get; } = [];
}