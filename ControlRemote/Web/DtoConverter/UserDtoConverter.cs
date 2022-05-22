using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Dto;
using Application.Command;

namespace Web.DtoConverter
{
    public static class UserDtoConverter
    {
        public static UserDto UserTransferCommandConvertToUserDto(UserTransferCommand user)
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
                Role = user.Role
            };
        }

        public static UserCreateCommand RegisterModelConvertToUserCreateCommand(RegisterModel registerModel)
        {
            if(registerModel == null)
            {
                return null;
            }
            return new UserCreateCommand
            {
                Name = registerModel.Name,
                Login = registerModel.Login,
                Password = registerModel.Password,
                Role = "user",
            };
        }
    }
}
