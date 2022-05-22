using Application.Command;
using Application.CommandConverter;
using Domain.Entity;
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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(UserCreateCommand user)
        {
            try
            {
                if (user != null)
                {
                    await _userRepository.CreateUser(UserCommandConverter.UserCreateCommandConvertToUserEntity(user));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetLoginResult(string login, string password)
        {
            try
            {
                if (login == null || password == null)
                {
                    return null;
                }
                User user = await _userRepository.GetLoginModel(login, password);
                if (user == null)
                {
                    return null;
                }
                return user.Role;
            }
            catch
            {
                return null;
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
                User user = await _userRepository.GetRegisterModel(login);
                if (user == null)
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

        public async Task<List<UserTransferCommand>> GetUsers()
        {
            try
            {
                List<User> usersEntity = await _userRepository.GetUsers();
                List<UserTransferCommand> usersCommand = usersEntity.Select(data => UserCommandConverter.UserEntityConvertToUserTransferCommand(data)).ToList();
                if(usersCommand == null)
                {
                    return null;
                }
                return usersCommand;
            }
            catch
            {
                return null;
            }
        }
    }
}
