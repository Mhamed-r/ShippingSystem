

namespace Shipping.Api.Core.Domain.Models;

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //------------- ICollection From Order ------------------------------
        public virtual ICollection<Order> Orders { get; set; } = [];
    }

