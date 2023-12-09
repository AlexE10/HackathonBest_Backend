using DataLayer.Enums;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace DataLayer.Entities
{
    public class User : BaseEntity
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }
            
        public RoleType Role { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
