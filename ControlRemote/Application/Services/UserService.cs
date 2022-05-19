using Application.Command;
using Application.CommandConverter;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public async Task<bool> CreateUser(UserCreateCommand user)
        {
            try
            {
                if (user != null)
                {
                    await _userRepository.CreateUser(UserCommandConverter.ConvertToUserEntity(user));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> GetLoginResult(string login, string password)
        {
            try
            {
                if(login == null || password == null)
                {
                    return false;
                }
                if(await _userRepository.GetLoginModel(login, password) == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> GetRegisterResult(string login)
        {
            try
            {
                if(login == null)
                {
                    return false;
                }
                if(await _userRepository.GetRegisterModel(login) == null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
