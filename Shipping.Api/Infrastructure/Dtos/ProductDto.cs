using System.ComponentModel.DataAnnotations;

namespace Shipping.Api.Infrastructure.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Weight { get; set; }

        [Required] 
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }

}
