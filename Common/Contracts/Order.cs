namespace Common.Contracts
{
    public class Order
    {
        public string Id { get; set; } = string.Empty;
        public DateTime OrderDateUtc { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = new();
        public List<Product> Products { get; set; } = [];
        public List<OrderLine> OrderLines { get; set; } = [];
    }
}
