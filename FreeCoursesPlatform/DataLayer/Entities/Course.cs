
using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Duration { get; set; }
        public Difficulty Difficulty { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
