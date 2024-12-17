using Microsoft.EntityFrameworkCore;

namespace My_WebAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<Order> Orders { get; private set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {     
        }
    }
}
