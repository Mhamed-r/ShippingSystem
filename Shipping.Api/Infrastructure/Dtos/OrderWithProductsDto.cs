using System.ComponentModel.DataAnnotations;

namespace Shipping.Api.Infrastructure.Dtos
{
    public class OrderWithProductsDto
    {
        public int Id { get; set; }
        [Required]
        public decimal TotalWeight { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Order cost must be greater than zero")]
        public decimal OrderCost { get; set; }
        public string? Notes { get; set; }
        public bool IsOutOfCityShipping { get; set; }


        // Ids From User
        public int? BranchId { get; set; }
        public int? RegionId { get; set; }
        public int? ShippingTypeId { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? CitySettingId { get; set; }
        //customer info
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        public string CustomerPhone1 { get; set; } = string.Empty;
        public string CustomerPhone2 { get; set; } = string.Empty;
        [Required]
        public string CustomerAddress { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        // merchant id
        [Required]
        public string MerchantId { get; set; } = string.Empty;

        //[Required]
        //public string CourierId { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        //list of products
        [MinLength(1, ErrorMessage = "At least one product is required")]

       
        public List<ProductDto> Products { get; set; } = new();
    }


}
