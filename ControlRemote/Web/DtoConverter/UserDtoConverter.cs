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
        public static UserDto ConvertToUserDto(Manager manager)
        {
            if(manager == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = manager.Id,
                Name = manager.Name,
                Login = manager.Login,
                Password = manager.Password
            };
        }
        public static Manager ConvertToUserEntiy(UserDto userDto)
        {
            if(userDto == null)
            {
                return null;
            }
            return new Manager
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Login = userDto.Login,
                Password = userDto.Password,
                Employers = null,
            };
        }
    }
}
