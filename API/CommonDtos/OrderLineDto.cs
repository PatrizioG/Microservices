namespace API.CommonDtos
{
    public class OrderLineDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
