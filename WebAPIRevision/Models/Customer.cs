using System;
using System.Collections.Generic;

namespace WebAPIRevision.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
