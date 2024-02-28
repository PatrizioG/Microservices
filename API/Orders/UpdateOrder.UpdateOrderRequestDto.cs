using System.ComponentModel.DataAnnotations;

namespace API.Orders;

public class UpdateOrderRequestDto
{
    [Required] public string? OrderId { get; set; }

    [Required] public List<UpdateOrderLineRequestDto> OrderLines { get; set; } = [];
}

public class UpdateOrderLineRequestDto
{
    [Required] public string? OrderLineId { get; set; }

    [Required] public double Quantity { get; set; }
}