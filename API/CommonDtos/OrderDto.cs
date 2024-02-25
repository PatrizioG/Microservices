namespace API.CommonDtos
{
    public class OrderDto
    {
        public DateTime OrderDateUtc { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public List<OrderLineDto> OrderLines { get; set; } = new();
    }
}
