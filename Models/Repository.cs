using Microsoft.EntityFrameworkCore;

namespace My_WebAPI.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public ApplicationDbContext Context { get; }

        public Repository(ApplicationDbContext dbContext)
        {
            Context = dbContext;
        }

        public virtual async Task<IList<T>> GetAllAsync(bool tracking = true)
        {
            DbSet<T> set = Context.Set<T>();
            return tracking ? await set.ToListAsync() : await set.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task AddAsync(T obj)
        {
            await Context.Set<T>().AddAsync(obj);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> objs)
        {
            await Context.Set<T>().AddRangeAsync(objs);
        }

        public virtual void Update(T obj)
        {
            Context.Set<T>().Update(obj);
        }

        public virtual void Remove(T obj)
        {
            Context.Set<T>().Remove(obj);
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}