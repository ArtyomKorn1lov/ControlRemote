using Domain.Entity;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ControlRemoteDbContext _controlRemoteDbContext;

        public UserRepository(ControlRemoteDbContext context)
        {
            _controlRemoteDbContext = context;
        }

        public async Task CreateUser(User user)
        {
            await _controlRemoteDbContext.Set<User>().AddAsync(user);
        }

        public async Task<User> GetLoginModel(string login, string password)
        {
            return await _controlRemoteDbContext.Set<User>().FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        }

        public async Task<User> GetRegisterModel(string login)
        {
            return await _controlRemoteDbContext.Set<User>().FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _controlRemoteDbContext.Set<User>().ToListAsync();
        }
    }
}
