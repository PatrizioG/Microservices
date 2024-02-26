using System.ComponentModel.DataAnnotations;

namespace API.Orders
{
    public class GetOrderByIdRequestDto
    {
        [Required]
        public string? Id { get; set; }
    }
}
