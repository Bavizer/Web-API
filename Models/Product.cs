using System.ComponentModel.DataAnnotations;

namespace My_WebAPI.Models
{
    public class Product
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public string Name { get; set; }
    }
}
