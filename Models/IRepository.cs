namespace My_WebAPI.Models
{
    public interface IRepository<T> where T : class
    {
        public ApplicationDbContext Context { get; }
        public Task<IList<T>> GetAllAsync(bool tracking = true);
        public Task<T?> GetAsync(int id);
        public Task AddAsync(T obj);
        public Task AddRangeAsync(IEnumerable<T> objs);
        public void Update(T obj);
        public void Remove(T obj);
        public Task SaveAsync();
    }
}
