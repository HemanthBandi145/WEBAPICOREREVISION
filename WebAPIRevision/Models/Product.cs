using System;
using System.Collections.Generic;

namespace WebAPIRevision.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
