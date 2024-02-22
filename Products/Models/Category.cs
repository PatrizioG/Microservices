namespace Products.Models
{
    internal class Category
    {
        public string Id { get; set; }
        public string? FatherId { get; set; }
        public Category? Father { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; } = [];
    }
}
