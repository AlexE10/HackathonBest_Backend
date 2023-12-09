using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string FirstAndLastName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public RoleType Role { get; set; }
    }
}
