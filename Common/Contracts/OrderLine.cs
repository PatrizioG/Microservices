namespace Common.Contracts
{
    public class OrderLine
    {
        public string Id { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}
