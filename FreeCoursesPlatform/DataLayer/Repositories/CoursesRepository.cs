using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class CoursesRepository : RepositoryBase<Course>
    {
        private readonly AppDbContext dbContext;
        public CoursesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
