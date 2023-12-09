using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using DataLayer.Entities;

namespace Core.Mapping
{
    public static class UserMappingExtensions
    {
        public static User ToUser(this RegisterDto registerDto)
        {
            return new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = registerDto.PasswordHash,
                Role = registerDto.Role,
                Courses = registerDto.Courses,
            };
        }
    }
}
