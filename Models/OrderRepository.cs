using Microsoft.EntityFrameworkCore;

namespace My_WebAPI.Models
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IList<Order>> GetAllAsync(bool tracking = true)
        {
            var result = Context.Orders.Include(o => o.Product).Include(o => o.Customer);
            return tracking ? await result.ToListAsync() : await result.AsNoTracking().ToListAsync();
        }

        public override async Task<Order?> GetAsync(int id)
        {
            return await Context.Orders.Include(o => o.Product).Include(o => o.Customer).SingleOrDefaultAsync(o => o.Id == id);
        }
    }
}
