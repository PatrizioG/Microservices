namespace Common.Contracts;

public class UpdateOrder
{
    public string? OrderId { get; set; }
    public List<UpdateOrderLine> OrderLines { get; set; } = [];
}