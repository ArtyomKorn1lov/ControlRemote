using Application.Command;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandConverter
{
    public static class UserCommandConverter
    {
        public static User ConvertToUserEntity(UserCreateCommand user)
        {
            if(user == null)
            {
                return null;
            }
            return new User
            {
                Name = user.Name,
                Login = user.Login,
                Password = user.Password,
                Role = user.Role,
                Employers = null
            };
        }
    }
}
