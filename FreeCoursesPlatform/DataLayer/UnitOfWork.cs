using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UnitOfWork
    {
        public UsersRepository Users { get; }
        public CategoriesRepository Categories { get; }

        public CoursesRepository Courses { get; }


        private readonly AppDbContext _dbContext;

        public UnitOfWork
        (
            AppDbContext dbContext,
            CategoriesRepository categoriesRepository,
            UsersRepository usersRepository,
            CoursesRepository coursesRepository

        )
        {
            _dbContext = dbContext;
            Users = usersRepository;
            Categories = categoriesRepository;
            Courses = coursesRepository;

        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}
