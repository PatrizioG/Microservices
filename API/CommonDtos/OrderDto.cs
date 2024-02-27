namespace API.CommonDtos;

public class OrderDto
{
    public string OrderId { get; set; } = string.Empty;
    public DateTime OrderDateUtc { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserSurname { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public List<OrderLineDto> OrderLines { get; set; } = [];
}
