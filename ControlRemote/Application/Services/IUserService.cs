using Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<string> GetLoginResult(string login, string password);
        Task<bool> GetRegisterResult(string login);
        Task<bool> CreateUser(UserCreateCommand user);
    }
}
