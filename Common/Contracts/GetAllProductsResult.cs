namespace Common.Contracts
{
    public class Category
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Category? Father { get; set; }
    }

    public class Product
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public Category Category { get; set; } = new();
    }

    public class GetAllProductsResult
    {
        public List<Product> Products { get; set; } = [];
    }
}
