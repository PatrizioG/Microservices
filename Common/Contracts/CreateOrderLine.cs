namespace Common.Contracts;

public class CreateOrderLine
{
    public string ProductId { get; set; } = string.Empty;
    public double Quantity { get; set; }
}