using System.ComponentModel.DataAnnotations;

namespace My_WebAPI.Models
{
    public class Order
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public uint Amount { get; set; }

        [Required]
        public Customer Customer { get; set; }
    }
}
