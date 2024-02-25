namespace Orders.Models
{
    public class OrderLineEntity
    {
        public string Id { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
