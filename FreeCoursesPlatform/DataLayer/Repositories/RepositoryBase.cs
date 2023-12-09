using DataLayer.Entities;

using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class RepositoryBase<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<T?> GetById(int? id)
        {
            if (id == null) throw new ArgumentNullException();
            return await _dbSet.FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            // _appDbContext.ChangeTracker.Clear();
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     This method will actually remove the row from the database.
        /// </summary>
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await GetRecords().ToListAsync();
        }

        public bool Any(Func<T, bool> expression)
        {
            return GetRecords().Any(expression);
        }

        protected IQueryable<T> GetRecords()
        {
            return _dbSet.AsQueryable<T>();
        }
    }
}