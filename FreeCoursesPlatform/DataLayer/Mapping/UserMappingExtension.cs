using DataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace DataLayer.Mapping
{
    internal class UserMappingExtension
    {
        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstAndLastName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
            };
        }

        public static List<UserDto> ToUserDtos(List<User> users)
        {
            return users.Select(ToDto).ToList();
        }
    }
}
