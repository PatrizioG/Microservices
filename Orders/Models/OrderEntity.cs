namespace Orders.Models
{
    public class OrderEntity
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime OrderDateUtc { get; set; }
        public ICollection<OrderLineEntity> Lines { get; set; } = [];
    }
}
