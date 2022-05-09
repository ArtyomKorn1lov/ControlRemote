using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Dto;

namespace Web.DtoConverter
{
    public static class UserDtoConverter
    {
        public static UserDto ConvertToUserDto(User user)
        {
            if(user == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                Password = user.Password,
                Role = user.Role
            };
        }
        public static User ConvertToUserEntiy(UserDto userDto)
        {
            if(userDto == null)
            {
                return null;
            }
            return new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Login = userDto.Login,
                Password = userDto.Password,
                Role = userDto.Role,
                Employers = null
            };
        }
    }
}
