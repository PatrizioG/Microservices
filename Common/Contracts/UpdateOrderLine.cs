namespace Common.Contracts;

public class UpdateOrderLine
{
    public string? OrderLineId { get; set; }
    public double Quantity { get; set; }
}