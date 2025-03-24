using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shipping.Api.Infrastructure.Dtos
{
    public class ProductDto
    {


        public string Name { get; set; } = string.Empty;

        public decimal Weight { get; set; }

        public int Quantity { get; set; } = 0;
    }

}
