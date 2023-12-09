using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class UsersRepository: RepositoryBase<User>
    {
        private readonly AppDbContext dbContext;
        public UsersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
