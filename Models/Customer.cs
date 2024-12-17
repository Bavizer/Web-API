using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace My_WebAPI.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
