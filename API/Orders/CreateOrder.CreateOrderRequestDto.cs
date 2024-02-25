using System.ComponentModel.DataAnnotations;

namespace API.Orders
{
    public class CreateOrderRequestDto
    {
        [Required]
        public string? UserId { get; set; }

        [Required]
        public List<string> ProductIds { get; set; } = [];
    }
}
