using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class UsersRepository: RepositoryBase<User>
    {
        private readonly AppDbContext dbContext;
        public UsersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<User> GetByIdWithCoursesAsync(int id)
        {
            return await _dbContext.Users
                                   .Include(u => u.CreatedCourses) // Eager loading CreatedCourses
                                   .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByIdWithEnrolledCourses(int id)
        {
            return await _dbContext.Users
                                   .Include(u => u.EnrolledCourses) // Eager loading EnrolledCourses
                                   .FirstOrDefaultAsync(u => u.Id == id);
        }

    }
}
