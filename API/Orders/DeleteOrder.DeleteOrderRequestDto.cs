using System.ComponentModel.DataAnnotations;

namespace API.Orders;

public class DeleteOrderRequestDto
{
    [Required] public string? Id { get; set; }
}