using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetLoginModel(string login, string password);
        Task<User> GetRegisterModel(string login);
        Task CreateUser(User user);
        Task<List<User>> GetUsers();
    }
}
