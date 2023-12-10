using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class CoursesRepository : RepositoryBase<Course>
    {
        private readonly AppDbContext dbContext;
        public CoursesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Course>> GetAllWithUsersAsync()
        {
            return await _dbContext.Courses
                                   .Include(c => c.Users) // Eager loading Users
                                   .ToListAsync();
        }

        public async Task<Course> GetByIdWithUsersAsync(int id)
        {
            return await _dbContext.Courses
                                   .Include(c => c.Users) // Eager loading Users
                                   .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
