using System.ComponentModel.DataAnnotations;

namespace WebAPIRevision.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; } // Used only for updates, not for inserts
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty ;
        public string Address { get; set; } = string.Empty;
    }
}
