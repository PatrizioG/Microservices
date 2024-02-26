using System.ComponentModel.DataAnnotations;

namespace API.Orders
{
    public class CreateOrderRequestDto
    {
        [Required]
        public string? UserId { get; set; }

        [Required]
        public List<CreateOrderLineRequestDto> OrderLines { get; set; } = [];
    }

    public class CreateOrderLineRequestDto
    {
        [Required]
        public string? ProductId { get; set; }

        [Required]
        public double Quantity { get; set; }
    }
}
