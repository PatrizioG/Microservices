namespace Common.Contracts;

public class Product
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public Category Category { get; set; } = new();
}
